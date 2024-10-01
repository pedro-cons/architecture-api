using System;
using System.Collections.Generic;

namespace Domain.ViewModels
{
    /// <summary>
    /// Return standardization for the API client.
    /// </summary>
    public class Response<T> : Response
    {
        /// <summary>
        /// Requested content.
        /// </summary>
        public T Data { get; set; }
    }


    /// <summary>
    /// Return standardization for the API client.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Create an instance of <see cref="Response"/>.
        /// </summary>
        /// <param name="withTracker">Determines whether an identifier will be sent to the client for exception tracking.</param>
        public Response(bool withTracker = false)
        {
            if (withTracker)
                this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Determines whether the answer produced results (correctly) or not.
        /// </summary>
        public bool Success
        {
            get
            {
                return (this.Error?.Count ?? 0) == 0;
            }
        }

        /// <summary>
        /// Date and time the response was generated, to assist in tracking exceptions.
        /// </summary>
        public DateTime Horario { get; set; } = DateTime.Now;

        /// <summary>
        /// Unique identifier of the response, when fatal exception.
        /// </summary>
        public Guid? Id { get; private set; }

        /// <summary>
        /// Alertas/avisos sobre o retorno. Os alertas completos podem ser obtidos pelo log no serviço.
        /// </summary>
        public List<string> Error { get; set; } = new List<string>();

    }

}
