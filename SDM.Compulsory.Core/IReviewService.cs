using System.Collections.Generic;

namespace SDM.Compulsory.Core
{
    public interface IReviewService
    {
        /// <summary>
        ///  On input N, what are the number of reviews from reviewer N
        /// </summary>
        /// <param name="reviewer">N</param>
        /// <returns>The number of reviews from reviewer N</returns>
        int GetNumberOfReviewsFromReviewer(int reviewer);
        
        /// <summary>
        /// On input N, what is the average rate that reviewer N had given? 
        /// </summary>
        /// <param name="reviewer">N</param>
        /// <returns>The average rate that reviewer N had given</returns>
        double GetAverageRateFromReviewer(int reviewer);
        
        /// <summary>
        /// On input N and R, how many times has reviewer N given rate R?
        /// </summary>
        /// <param name="reviewer">N</param>
        /// <param name="rate">R</param>
        /// <returns>How many times has reviewer N given rate R</returns>
        int GetNumberOfRatesByReviewer(int reviewer, int rate);
        
        /// <summary>
        /// On input N, how many have reviewed movie N
        /// </summary>
        /// <param name="movie">N</param>
        /// <returns>How many have reviewed movie N</returns>
        int GetNumberOfReviews(int movie);
        
        /// <summary>
        /// On input N, what is the average rate the movie N had received?
        /// </summary>
        /// <param name="movie">N</param>
        /// <returns>What is the average rate the movie N had received?</returns>
        double GetAverageRateOfMovie(int movie);
        
        /// <summary>
        /// On input N and R, how many times had movie N received rate R?
        /// </summary>
        /// <param name="movie">N</param>
        /// <param name="rate">R</param>
        /// <returns>How many times had movie N received rate R</returns>
        int GetNumberOfRates(int movie, int rate);
        
        /// <summary>
        /// What is the id(s) of the movie(s) with the highest number of top rates (5)?
        /// </summary>
        /// <returns>The id(s) of the movie(s) with the highest number of top rates</returns>
        List<int> GetMoviesWithHighestNumberOfTopRates();
        
        /// <summary>
        /// What reviewer(s) had done most reviews? 
        /// </summary>
        /// <returns>A list sorted by what reviewer has done most</returns>
        List<int> GetMostProductiveReviewers();
        
        /// <summary>
        ///  On input N, what is top N of movies? The score of a movie is its average rate
        /// </summary>
        /// <param name="amount">N</param>
        /// <returns>The list of top movies</returns>
        List<int> GetTopRatedMovies(int amount);
        
        /// <summary>
        /// On input N, what are the movies that reviewer N has reviewed? The list should be sorted decreasing by rate first, and date secondly.
        /// </summary>
        /// <param name="reviewer">N</param>
        /// <returns>The list of reviewers top movies</returns>
        List<int> GetTopMoviesByReviewer(int reviewer);
        
        /// <summary>
        /// On input N, who are the reviewers that have reviewed movie N? The list should be sorted decreasing by rate first, and date secondly.
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>Reviewers that have reviewed a specific movie</returns>
        List<int> GetReviewersByMovie(int movie);
    }
}