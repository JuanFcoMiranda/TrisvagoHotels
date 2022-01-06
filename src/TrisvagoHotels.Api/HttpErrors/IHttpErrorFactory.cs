using System;

namespace TrisvagoHotels.Api.HttpErrors;

public interface IHttpErrorFactory {
    HttpError CreateFrom(Exception exception);
}