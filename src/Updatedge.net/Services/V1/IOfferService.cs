using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.Common.Models.Offer;

namespace Updatedge.net.Services.V1
{
    public interface IOfferService
    {
        /// <summary>
        /// Create an offer
        /// </summary>
        /// <param name="offer">The offer to send</param>
        /// <returns>Offer Id</returns>
        /// <exception cref="FlurlHttpException">Thrown if the http response is not in the 2xx range.</exception>
        Task<string> CreateOfferAsync(CreateOffer offer);

        /// <summary>
        /// Get an offer
        /// </summary>
        /// <param name="id">The id of the Offer</param>
        /// <returns>The offer</returns>
        /// <exception cref="FlurlHttpException">Thrown if the http response is not in the 2xx range.</exception>
        Task<Offer> GetOfferAsync(string id);

        /// <summary>
        /// Withdraw an offer
        /// </summary>
        /// <param name="id">The id of the offer</param>
        /// <returns>204 no content</returns>
        /// <exception cref="FlurlHttpException">Thrown if the http response is not in the 2xx range.</exception>
        Task<bool> WithdrawOfferAsync(string id);

        /// <summary>
        /// Complete and offer
        /// </summary>
        /// <param name="id">The id of the offer</param>
        /// <param name="workerIds">The ids of the worker you want to notify as being successful.</param>
        /// <param name="workerIds">The total gross pay to pay to the worker upon completion</param>
        /// <returns>204 no content</returns>
        /// <exception cref="FlurlHttpException">Thrown if the http response is not in the 2xx range.</exception>
        Task<bool> CompleteOfferAsync(string id, IEnumerable<string> workerIds, decimal? totalGrossPay);

        /// <summary>
        /// Adds workers to an existing offer
        /// </summary>
        /// <param name="id">The offer id</param>
        /// <param name="alterations">An object containing the changes to make to the offer</param>
        /// <returns>204 no content</returns>
        /// <exception cref="FlurlHttpException">Thrown if the http response is not in the 2xx range.</exception>
        Task<bool> AlterOfferAsync(string id, AlterOffer alterations);

        Task<bool> WithdrawOfferFromWorkerAsync(string id, string workerId);

        Task<bool> WithdrawOfferAsync(OfferWithdraw request);
        Task<bool> DeleteEventFromOfferAsync(EventDelete request);
        Task<bool> DeleteEventsFromOfferAsync(List<EventDelete> request);
        Task<string> AlterConfirmedOfferAsync(string id, AlterOffer alterations);
    }
}
