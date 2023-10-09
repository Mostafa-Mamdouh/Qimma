///Bundles/AngularOutput/assets/i18n/  ======= assets/i18n

//dist/angular ===== ../Bundles/AngularOutput

import { Injectable, Inject, forwardRef } from '@angular/core';
//import { Headers, Http, Response } from "@angular/http";
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { TranslateLoader } from '@ngx-translate/core';
import { Observable } from 'rxjs/internal/Observable';


 
@Injectable()
export class CustomTranslateLoader implements TranslateLoader {
  contentHeader = new HttpHeaders({ "Content-Type": "application/json", "Access-Control-Allow-Origin": "*" });

  constructor(private httpClient: HttpClient) { }
  getTranslation(lang: string): Observable<any> {
    //debugger;
    //var apiAddress = "assets/i18n/" + lang + ".json";//"Bundles/AngularOutput/assets/i18n/" + lang + ".json";
    //var LocalizationLocation = "/assets/i18n/";//"/Bundles/AngularOutput/assets/i18n/";

    var apiAddress = "/Bundles/AngularOutput/assets/i18n/" + lang + ".json";
    var LocalizationLocation = "/Bundles/AngularOutput/assets/i18n/";
    if (lang == undefined || lang == "undefined") {
      var lang = 'ar';
      //  failed to retrieve from api, switch to local
      //debugger;
      var CurrentServiceLang = localStorage.getItem('currentLang') == "undefined" ? lang : localStorage.getItem('currentLang');
      return Observable.create((observer: any) => {
        this.httpClient.get(LocalizationLocation + lang + ".json").subscribe((res: any) => {
          observer.next(res);
          observer.complete();
        })
      });
    }
    var CurrentServiceLang = localStorage.getItem('currentLang') == "undefined" ? lang : localStorage.getItem('currentLang');
    return Observable.create((observer: any) => {
      this.httpClient.get(lang == "undefined" ? LocalizationLocation + CurrentServiceLang + ".json" : apiAddress, { headers: this.contentHeader }).subscribe((res: any) => {
        observer.next(res);
        observer.complete();
      },
        error => {

          var lang = 'ar';
          //  failed to retrieve from api, switch to local
          //debugger;
          var CurrentServiceLang = localStorage.getItem('currentLang') == "undefined" ? lang : localStorage.getItem('currentLang');
          this.httpClient.get(LocalizationLocation + lang + ".json").subscribe((res: any) => {
            observer.next(res);
            observer.complete();
          })
        }
      );
    });
  }
}
