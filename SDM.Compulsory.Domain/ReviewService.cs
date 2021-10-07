using System;
using System.Collections.Generic;
using System.Linq;
using SDM.Compulsory.Core;

namespace SDM.Compulsory.Domain
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;

        public ReviewService(IReviewRepository repository)
        {
            _repository = repository; 
        }

        /// <inheritdoc/>
        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            List<Review> results = _repository.GetAll().Where(x => x.Reviewer == reviewer).ToList();
            return results.Count;
        }
       
        public double GetAverageRateFromReviewer(int reviewer)
        {
            List<Review> results = _repository.GetAll().Where(x => x.Reviewer == reviewer).ToList();

            if (results.Count == 0)
                return 0;

            double gradeAvg = 0;
            foreach (var review in results)
            {
                gradeAvg += review.Grade;
            }

            return gradeAvg / results.Count;
        }
 
        /// <inheritdoc/>
        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            List<Review> results = _repository.GetAll().Where(x => x.Reviewer == reviewer && x.Grade == rate).ToList();
            return results.Count;
        }
        
        /// <inheritdoc/>
        public int GetNumberOfReviews(int movie)
        {
            List<Review> results = _repository.GetAll().Where(x => x.Movie == movie).ToList();
            
            if (results.Count == 0)
                return 0;
            
            return results.Count;
        }

        /// <inheritdoc/>
        public double GetAverageRateOfMovie(int movie)
        {
            List<Review> results = _repository.GetAll().Where(x => x.Movie == movie).ToList();

            if (results.Count == 0)
                return 0;
            
            double movieAvgGrade = 0;
            foreach (var review in results)
            {
                movieAvgGrade += review.Grade;
            }
            
            return movieAvgGrade/results.Count;
            
        }
        
        /// <inheritdoc/>
        public int GetNumberOfRates(int movie, int rate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            throw new NotImplementedException();
        }
        
        /// <inheritdoc/>
        public List<int> GetMostProductiveReviewers()
        {
            throw new NotImplementedException();
        }
        
        /// <inheritdoc/>
        public List<int> GetTopRatedMovies(int amount)
        {
            throw new NotImplementedException();
        }
        
        /// <inheritdoc/>
        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            throw new NotImplementedException();
        }
        
        /// <inheritdoc/>
        public List<int> GetReviewersByMovie(int movie)
        {
            throw new NotImplementedException();
        }
    }
}