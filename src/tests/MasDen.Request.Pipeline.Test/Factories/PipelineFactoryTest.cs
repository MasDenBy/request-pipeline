using System;
using System.Collections.Generic;

using MasDen.Request.Pipeline.Factories;

using NSubstitute;

using NUnit.Framework;

namespace MasDen.Request.Pipeline.Test.Factories
{
	[TestFixture]
	public class PipelineFactoryTest
	{
		#region Private Fields

		private PipelineFactory factory;

		private IServiceProvider serviceProvider;

		#endregion

		#region Test Context

		[SetUp]
		public void TestInitialize()
		{
			this.serviceProvider = Substitute.For<IServiceProvider>();
			this.factory = new PipelineFactory(this.serviceProvider);
		}

		#endregion

		#region Tests

		[Test]
		public void CreateIfRequestHandlersDoesNotRegisteredShouldThrowExceptionTest()
		{
			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => this.factory.Create<TestRequest, object>());
		}

		[Test]
		public void CreateIfRequestHandlerDoesNotExistsShouldThrowExceptionTest()
		{
			// Arrange
			var mockRequests = new List<IRequestHandler<TestRequest, object>>
			{
				Substitute.For<IPreRequestHandler<TestRequest, object>>()
			};

			this.serviceProvider.GetService(typeof(IEnumerable<IRequestHandler<TestRequest, object>>)).Returns(mockRequests);

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => this.factory.Create<TestRequest, object>());
		}

		[Test]
		public void CreateShouldReturnPipelineWithRequestHandlersTest()
		{
			// Arrange
			var mockRequests = new List<IRequestHandler<TestRequest, object>>
			{
				new TestRequestHandler(),
				Substitute.For<IPreRequestHandler<TestRequest, object>>(),
				Substitute.For<IPostRequestHandler<TestRequest, object>>(),
				Substitute.For<IFinishRequestHandler<TestRequest, object>>()
			};

			this.serviceProvider.GetService(typeof(IEnumerable<IRequestHandler<TestRequest, object>>)).Returns(mockRequests);

			// Act
			var pipeline = this.factory.Create<TestRequest, object>();

			// Assert
			Assert.IsNotNull(pipeline);
			Assert.IsNotNull(pipeline.RequestHandler);
			Assert.IsNotEmpty(pipeline.PreRequestHandlers);
			Assert.IsNotEmpty(pipeline.PostRequestHandlers);
			Assert.IsNotEmpty(pipeline.FinishRequestHandlers);
		}

		[Test]
		public void CreateIfNotRegisteredShouldReturnPipelineWithEmptyRequestHandlersTest()
		{
			// Arrange
			var mockRequests = new List<IRequestHandler<TestRequest, object>>
			{
				new TestRequestHandler()
			};

			this.serviceProvider.GetService(typeof(IEnumerable<IRequestHandler<TestRequest, object>>)).Returns(mockRequests);

			// Act
			var pipeline = this.factory.Create<TestRequest, object>();

			// Assert
			Assert.IsNotNull(pipeline);
			Assert.IsNotNull(pipeline.RequestHandler);
			Assert.IsEmpty(pipeline.PreRequestHandlers);
			Assert.IsEmpty(pipeline.PostRequestHandlers);
			Assert.IsEmpty(pipeline.FinishRequestHandlers);
		}

		#endregion
	}
}