using System;
using System.Collections.Generic;

using NSubstitute;

using NUnit.Framework;

namespace MasDen.Request.Pipeline.Test
{
	[TestFixture]
	public class PipelineTest
	{
		#region Tests

		[Test]
		public void ConstructorIfRequestHandlerIsNullShouldThrowExceptionTest()
		{
			// Act & Assert
			var exception = Assert.Throws<ArgumentNullException>(() => new Pipeline<TestRequest, object>(
				null,
				PipelineTest.CreatePreRequestHandlers(),
				PipelineTest.CreatePostRequestHandlers(),
				PipelineTest.CreateFinishRequestHandlers()));

			Assert.AreEqual("Value cannot be null. (Parameter 'requestHandler')", exception.Message);
		}

		[Test]
		public void ConstructorIfPreRequestHandlersAreNullShouldThrowExceptionTest()
		{
			// Act & Assert
			var exception = Assert.Throws<ArgumentNullException>(() => new Pipeline<TestRequest, object>(
				Substitute.For<IRequestHandler<TestRequest, object>>(),
				null,
				PipelineTest.CreatePostRequestHandlers(),
				PipelineTest.CreateFinishRequestHandlers()));

			Assert.AreEqual("Value cannot be null. (Parameter 'preRequestHandlers')", exception.Message);
		}

		[Test]
		public void ConstructorIfPostRequestHandlersAreNullShouldThrowExceptionTest()
		{
			// Act & Assert
			var exception = Assert.Throws<ArgumentNullException>(() => new Pipeline<TestRequest, object>(
				Substitute.For<IRequestHandler<TestRequest, object>>(),
				PipelineTest.CreatePreRequestHandlers(),
				null,
				PipelineTest.CreateFinishRequestHandlers()));

			Assert.AreEqual("Value cannot be null. (Parameter 'postRequestHandlers')", exception.Message);
		}

		[Test]
		public void ConstructorIfFinishRequestHandlersAreNullShouldThrowExceptionTest()
		{
			// Act & Assert
			var exception = Assert.Throws<ArgumentNullException>(() => new Pipeline<TestRequest, object>(
				Substitute.For<IRequestHandler<TestRequest, object>>(),
				PipelineTest.CreatePreRequestHandlers(),
				PipelineTest.CreatePostRequestHandlers(),
				null));

			Assert.AreEqual("Value cannot be null. (Parameter 'finishRequestHandlers')", exception.Message);
		}

		[Test]
		public void ConstructorTest()
		{
			// Arrange & Act
			var pipeline = new Pipeline<TestRequest, object>(
				Substitute.For<IRequestHandler<TestRequest, object>>(),
				PipelineTest.CreatePreRequestHandlers(),
				PipelineTest.CreatePostRequestHandlers(),
				PipelineTest.CreateFinishRequestHandlers());

			// Assert
			Assert.IsNotNull(pipeline);
			Assert.IsNotNull(pipeline.RequestHandler);
			Assert.IsNotEmpty(pipeline.PreRequestHandlers);
			Assert.IsNotEmpty(pipeline.PostRequestHandlers);
			Assert.IsNotEmpty(pipeline.FinishRequestHandlers);
		}

		#endregion

		#region Private Methods

		private static IEnumerable<IPreRequestHandler<TestRequest, object>> CreatePreRequestHandlers()
		{
			return new List<IPreRequestHandler<TestRequest, object>>
			{
				Substitute.For<IPreRequestHandler<TestRequest, object>>()
			};
		}

		private static IEnumerable<IPostRequestHandler<TestRequest, object>> CreatePostRequestHandlers()
		{
			return new List<IPostRequestHandler<TestRequest, object>>
			{
				Substitute.For<IPostRequestHandler<TestRequest, object>>()
			};
		}

		private static IEnumerable<IFinishRequestHandler<TestRequest, object>> CreateFinishRequestHandlers()
		{
			return new List<IFinishRequestHandler<TestRequest, object>>
			{
				Substitute.For<IFinishRequestHandler<TestRequest, object>>()
			};
		}

		#endregion
	}
}