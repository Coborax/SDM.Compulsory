using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Moq;
using SDM.Compulsory.Core;
using SDM.Compulsory.Domain;
using Xunit;

namespace SDM.Compulsory.Test.xUnit
{
    public class ReviewServiceTest
    {

        private Dictionary<int, Review> _reviews;
        private Mock<IReviewRepository> _mockRepo;

        public ReviewServiceTest()
        {
            _reviews = new Dictionary<int, Review>();
            _mockRepo = new Mock<IReviewRepository>();
            
            _mockRepo.SetupAllProperties();
            _mockRepo.Setup(x => x.Add(It.IsAny<Review>()))
                .Callback<Review>(y =>_reviews.Add(y.Id, y));
            
            _mockRepo.Setup(x => x.Delete(It.IsAny<Review>()))
                .Callback<Review>(y =>_reviews.Remove(y.Id));
            
            _mockRepo.Setup(x => x.GetbyId(It.IsAny<int>()))
                .Returns<int>(y => _reviews.ContainsKey(y) ? _reviews[y] : null);
            
            _mockRepo.Setup(x => x.GetAll())
                .Returns(() => new List<Review>(_reviews.Values));
            
            _mockRepo.Setup(x => x.Update(It.IsAny<Review>()))
                .Callback<Review>(y => _reviews[y.Id] = y)
                .Returns<Review>(y => _reviews[y.Id]);
        }

        private void SetupData(IReviewRepository repo)
        {
            // Reviewer ID: 1
            repo.Add(new Review {Id = 1, Reviewer = 1, Movie = 1, Grade = 4, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 2, Reviewer = 1, Movie = 2, Grade = 3, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 3, Reviewer = 1, Movie = 3, Grade = 1, ReviewDate = DateTime.Now});
            
            // Reviewer ID: 2
            repo.Add(new Review {Id = 4, Reviewer = 2, Movie = 4, Grade = 4, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 5, Reviewer = 2, Movie = 2, Grade = 2, ReviewDate = DateTime.Now});

            // Reviewer ID: 3
            repo.Add(new Review {Id = 7, Reviewer = 3, Movie = 1, Grade = 5, ReviewDate = DateTime.Now});
        }
        
        private void SetupDataWithDuplicateGrades(IReviewRepository repo)
        {
            // Reviewer ID: 1
            repo.Add(new Review {Id = 1, Reviewer = 1, Movie = 1, Grade = 4, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 2, Reviewer = 1, Movie = 2, Grade = 3, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 3, Reviewer = 1, Movie = 3, Grade = 4, ReviewDate = DateTime.Now});
            
            // Reviewer ID: 2
            repo.Add(new Review {Id = 4, Reviewer = 2, Movie = 4, Grade = 2, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 5, Reviewer = 2, Movie = 2, Grade = 2, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 6, Reviewer = 2, Movie = 3, Grade = 2, ReviewDate = DateTime.Now});
            
            // Reviewer ID: 3
            repo.Add(new Review {Id = 7, Reviewer = 3, Movie = 1, Grade = 5, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 8, Reviewer = 3, Movie = 5, Grade = 3, ReviewDate = DateTime.Now});
            
        }
        
        private void SetupDataWithTopRate(IReviewRepository repo)
        {
            // Reviewer ID: 1
            repo.Add(new Review {Id = 1, Reviewer = 1, Movie = 1, Grade = 4, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 2, Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 3, Reviewer = 1, Movie = 3, Grade = 5, ReviewDate = DateTime.Now});
            
            // Reviewer ID: 2
            repo.Add(new Review {Id = 4, Reviewer = 2, Movie = 4, Grade = 3, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 5, Reviewer = 2, Movie = 2, Grade = 5, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 6, Reviewer = 2, Movie = 3, Grade = 5, ReviewDate = DateTime.Now});
            
            // Reviewer ID: 3
            repo.Add(new Review {Id = 7, Reviewer = 3, Movie = 1, Grade = 5, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 8, Reviewer = 3, Movie = 5, Grade = 3, ReviewDate = DateTime.Now});
            repo.Add(new Review {Id = 9, Reviewer = 3, Movie = 3, Grade = 5, ReviewDate = DateTime.Now});
            
        }

        private void SetupDataEmpty(IReviewRepository repo)
        {
            // *Cricket sound*
        }
        
        [Theory]
        [InlineData(1,3)]
        [InlineData(2,2)]
        [InlineData(3,1)]
        [InlineData(4,0)]
        public void GetNumberOfReviewsFromReviewerTest(int id, int expected)
        {
            // Arrange
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupData(repo);
            
            // Act
            int result = reviewService.GetNumberOfReviewsFromReviewer(id);
            
            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 11.0/3.0)]
        [InlineData(2, 6.0/3.0)]
        [InlineData(3, 8.0/2.0)]
        [InlineData(4, 0.0)]
        public void GetAverageRateFromReviewerTest(int id, double expected)
        {
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupDataWithDuplicateGrades(repo);
            
            double result = reviewService.GetAverageRateFromReviewer(id);
            
            // Assert
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(1, 4, 2)]
        [InlineData(2, 2, 3)]
        [InlineData(3, 5, 1)]
        [InlineData(4, 1, 0)]
        public void GetNumberOfRatesByReviewerTest(int id, int rating, int expected)
        {
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupDataWithDuplicateGrades(repo);
            
            int result = reviewService.GetNumberOfRatesByReviewer(id, rating);
            
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(4, 1)]
        public void GetNumberOfReviewsTest(int movie, int expected)
        {
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupData(repo);
            
            int result = reviewService.GetNumberOfReviews(movie);
            
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(1, 9/2.0)]
        [InlineData(2, 5/2.0)]
        [InlineData(3, 1.0)]
        [InlineData(5, 0.0)]
        public void GetAverageRateOfMovieTest(int movie, double expected)
        {
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupData(repo);
            
            double result = reviewService.GetAverageRateOfMovie(movie);
            
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 2, 0)]
        [InlineData(2, 3, 1)]
        [InlineData(3, 4, 1)]
        public void GetNumberOfRatesTest(int movie, int rate, int expected)
        {
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupDataWithDuplicateGrades(repo);

            double result = reviewService.GetNumberOfRates(movie, rate);
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetMoviesWithHighestNumberOfTopRatesTest()
        {
            // Arrange
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            SetupDataWithTopRate(repo);
            List<int> expectedResult = new List<int> { 3, 2, 1};
            
            // Act
            List<int> result = reviewService.GetMoviesWithHighestNumberOfTopRates();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GetMoviesWithHighestNumberOfTopRatesEmptyTest()
        {
            // Arrange
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupDataEmpty(repo);
            
            List<int> expectedResult = new List<int> {Capacity = 0};
            
            // Act
            List<int> result = reviewService.GetMoviesWithHighestNumberOfTopRates();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GetMostProductiveReviewersTest()
        {
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupDataWithDuplicateGrades(repo);

            List<int> expectedResult = new List<int> { 1, 2, 3 };
            List<int> result = reviewService.GetMostProductiveReviewers();
            
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5, new[] {1, 3, 5, 2, 4})]
        [InlineData(4, new[] {1, 3, 5, 2})]
        [InlineData(1,new[] {1})]
        public void GetTopRatedMoviesTest(int amount, int[] expectedResult)
        {
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupDataWithDuplicateGrades(repo);

            List<int> result = reviewService.GetTopRatedMovies(amount);

            Assert.Equal(new List<int>(expectedResult), result);
        }

        [Theory]
        [InlineData(1, new[] {1, 3, 2})]
        [InlineData(2, new[] {3, 2, 4})]
        [InlineData(3, new[] {1, 5})]
        [InlineData(4, new int[0])]
        public void GetTopMoviesByReviewerTest(int reviewer, int[] expectedResult)
        {
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupDataWithDuplicateGrades(repo);
            
            List<int> result = reviewService.GetTopMoviesByReviewer(reviewer);

            Assert.Equal(new List<int>(expectedResult), result);
        }

        [Theory]
        [InlineData(1, new[] {1, 3})]
        [InlineData(2, new[] {1, 2})]
        [InlineData(3, new[] {1, 2})]
        [InlineData(4, new[] {2})]
        [InlineData(5, new[] {3})]
        public void GetReviewersByMovieTest(int movie, int[] expectedResult)
        {
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupDataWithDuplicateGrades(repo);
            
            List<int> result = reviewService.GetReviewersByMovie(movie);
            
            Assert.Equal(new List<int>(expectedResult), result);
        }

    }
}