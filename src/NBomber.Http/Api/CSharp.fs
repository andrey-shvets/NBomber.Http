namespace NBomber.Plugins.Http.CSharp

open System
open System.Net.Http
open System.Threading.Tasks
open System.Runtime.CompilerServices

open NBomber
open NBomber.Contracts
open NBomber.Http
open NBomber.Plugins.Http.FSharp

type Http =
    static member CreateRequest(method: string, url: string) = Http.createRequest method url

[<Extension>]
type HttpExt =
    [<Extension>]
    static member Get(url: string) =
        url |> Http.createRequest "GET"

    [<Extension>]
    static member Post(url: string) =
        url |> Http.createRequest "POST"

    [<Extension>]
    static member Put(url: string) =
        url |> Http.createRequest "PUT"

[<Extension>]
type HttpRequestExt =

    [<Extension>]
    static member WithHeader(req: HttpRequest, name: string, value: string) =
        req |> Http.withHeader name  value

    [<Extension>]
    static member WithVersion(req: HttpRequest, version: string) =
        req |> Http.withVersion(version)

    [<Extension>]
    static member WithBody(req: HttpRequest, body: HttpContent) =
        req |> Http.withBody(body)

    [<Extension>]
    static member WithCheck(req: HttpRequest, check: System.Func<HttpResponseMessage,Task<Response>>) =
        req |> Http.withCheck(fun response -> check.Invoke(response))

type HttpStep =

    static member Create(name: string,
                         createRequest: Func<IStepContext<Unit,Unit>, HttpRequest>) =
        HttpStep.create(name, createRequest.Invoke)

    static member Create(name: string,
                         createRequest: Func<IStepContext<Unit,Unit>, Task<HttpRequest>>) =
        HttpStep.create(name, createRequest.Invoke)

    static member Create(name: string,
                         createRequest: Func<IStepContext<Unit,Unit>, HttpRequest>,
                         completionOption: HttpCompletionOption) =
        HttpStep.create(name, Feed.empty, createRequest.Invoke, completionOption)

    static member Create(name: string,
                         createRequest: Func<IStepContext<Unit,Unit>, Task<HttpRequest>>,
                         completionOption: HttpCompletionOption) =
        HttpStep.create(name, Feed.empty, createRequest.Invoke, completionOption)

    static member Create(name: string,
                         feed: IFeed<'TFeedItem>,
                         createRequest: Func<IStepContext<unit,'TFeedItem>,HttpRequest>) =
        HttpStep.create(name, feed, createRequest.Invoke)

    static member Create(name: string,
                         feed: IFeed<'TFeedItem>,
                         createRequest: Func<IStepContext<unit,'TFeedItem>,Task<HttpRequest>>) =
        HttpStep.create(name, feed, createRequest.Invoke)

    static member Create(name: string,
                         feed: IFeed<'TFeedItem>,
                         createRequest: Func<IStepContext<unit,'TFeedItem>,HttpRequest>,
                         completionOption: HttpCompletionOption) =
        HttpStep.create(name, feed, createRequest.Invoke, completionOption)

    static member Create(name: string,
                         feed: IFeed<'TFeedItem>,
                         createRequest: Func<IStepContext<unit,'TFeedItem>,Task<HttpRequest>>,
                         completionOption: HttpCompletionOption) =
        HttpStep.create(name, feed, createRequest.Invoke, completionOption)
