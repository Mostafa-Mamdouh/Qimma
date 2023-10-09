import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { AppSettings } from '../AppSettings';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import * as $ from 'jquery';
export class ParameterClass {
  constructor(public ParameterName: any, public ParameterValue: any) {}
}

@Injectable()
export class Helper {
  WebApiUrl: string = AppSettings.WebApiUrl;

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  // private httpOptionsWithToken = {
  //   headers: new HttpHeaders({ 'Content-Type': 'application/json' }).set(
  //     'Authorization',
  //     'Bearer ' + this.auth.GetToken()
  //   ),
  // };

  httpOptionsNewDynamicObject = {
    headers: new HttpHeaders({}),
  };

  constructor(private httpclient: HttpClient, private router: Router) {}

  // Get Data using POST Method
  public GetRequest(url: string, parameters?: ParameterClass[]) {
    var Url: string = this.WebApiUrl + url;

    if (parameters != null && parameters.length > 0) {
      Url += '?';
      for (var _i = 0; _i < parameters.length; _i++) {
        if (_i != 0) Url += '&&';
        Url +=
          parameters[_i].ParameterName + '=' + parameters[_i].ParameterValue;
      }
    }

    return this.httpclient.post(Url, null, this.httpOptions);
  }

  // Get Data using POST Method with request body
  public GetRequestWithBody(url: string, body: any = null, parameters?: ParameterClass[]) {
    var Url: string = this.WebApiUrl + url;
    console.log(Url);

    if (parameters != null && parameters.length > 0) {
      Url += '?';
      for (var _i = 0; _i < parameters.length; _i++) {
        if (_i != 0) Url += '&&';
        Url +=
          parameters[_i].ParameterName + '=' + parameters[_i].ParameterValue;
      }
    }

    return this.httpclient.post(Url, body, this.httpOptions);
  }

  public AddRequest(url: string, body: any, parameters?: ParameterClass[]) {
    var Url: string = this.WebApiUrl + url;

    if (parameters != null && parameters.length > 0) {
      Url += '?';
      for (var _i = 0; _i < parameters.length; _i++) {
        if (_i != 0) Url += '&&';
        Url +=
          parameters[_i].ParameterName + '=' + parameters[_i].ParameterValue;
      }
    }

    return this.httpclient.post(Url, body, this.httpOptions);
  }
  public CallRequestNew(url: string, parameters?: string[]) {
    var Url: string = this.WebApiUrl + url;
    if (parameters != null && parameters.length > 0) {
      Url += "?";
      for (var _i = 0; _i < parameters.length; _i++) {
        if (_i != 0)
          Url += "&&";
        Url += parameters[_i];
      }
    }

    return this.httpclient.get(Url);
  }
  public PutRequest(
    url: string,
    body: any,
    id?: string | number | null,
    parameters?: ParameterClass[]
  ) {
    var Url: string = this.WebApiUrl + url;

    if (parameters != null && parameters.length > 0) {
      Url += '?';
      for (var _i = 0; _i < parameters.length; _i++) {
        if (_i != 0) Url += '&&';
        Url +=
          parameters[_i].ParameterName + '=' + parameters[_i].ParameterValue;
      }
    }

    if (id != null) Url += '/' + id;

    return this.httpclient.put(Url, body, this.httpOptions);
  }

  public DeleteRequest(url: string, id: string | number | null) {
    var Url: string = this.WebApiUrl + url;

    if (id != null) Url += '/' + id;

    return this.httpclient.delete(Url, this.httpOptions);
  }

  // Get Request From Custom Api
  public CallCustomApi(url: string, parameters?: string[]) {
    var Url: string = url;
    if (parameters != null && parameters.length > 0) {
      Url += '?';
      for (var _i = 0; _i < parameters.length; _i++) {
        if (_i != 0) Url += '&&';
        Url += parameters[_i];
      }
    }
    return this.httpclient.get(Url);
  }

  CreateFullUrl(url: string, parameters?: string[]) {
    var Url: string = this.WebApiUrl + url;
    if (parameters != null && parameters.length > 0) {
      Url += '?';
      for (var _i = 0; _i < parameters.length; _i++) {
        if (_i != 0) Url += '&&';
        Url += parameters[_i];
      }
    }
    return Url;
  }

  public baseUrl() {
    if (window.location.origin) return window.location.origin;

    return (
      window.location.protocol +
      '//' +
      window.location.hostname +
      (window.location.port ? ':' + window.location.port : '')
    );
  }

  public CreateRequestParameters(parmas: ParameterClass[]): string[] {
    var finalparams: string[] = [];
    for (var _i = 0; _i < parmas.length; _i++) {
      finalparams.push(
        parmas[_i].ParameterName + '=' + parmas[_i].ParameterValue
      );
    }
    return finalparams;
  }


 
}
