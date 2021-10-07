using System;
using System.Collections.Generic;
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
        [InlineData(1, 8/3.0)]
        [InlineData(2, 6/2)]
        [InlineData(3, 5/1)]
        [InlineData(4, 0)]
        public void GetAverageRateFromReviewerTest(int id, double expected)
        {
            IReviewRepository repo = _mockRepo.Object;
            IReviewService reviewService = new ReviewService(repo);
            
            SetupData(repo);
            
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
        
    }
}