using System;
using System.Collections.Generic;
using SDM.Compulsory.Core;

namespace SDM.Compulsory.Domain
{
    public class ReviewService : IReviewService
    {
        /// <inheritdoc/>
        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            throw new NotImplementedException();
        }
       
        public double GetAverageRateFromReviewer(int reviewer)
        {
            throw new NotImplementedException();
        }
 
        /// <inheritdoc/>
        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            throw new NotImplementedException();
        }
        
        /// <inheritdoc/>
        public int GetNumberOfReviews(int movie)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public double GetAverageRateOfMovie(int movie)
        {
            throw new NotImplementedException();
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