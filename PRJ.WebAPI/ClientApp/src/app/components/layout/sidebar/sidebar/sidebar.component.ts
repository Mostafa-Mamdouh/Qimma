import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { faPowerOff } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';
import { SharedService } from './../../../../services/Shared/shared.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  faPowerOff = faPowerOff;
  public lang: string = 'en';
  isCollapsed = true;
  htmlTag = this.document.getElementsByTagName('html')[0] as HTMLHtmlElement;

  constructor(
    public translate: TranslateService,
    private activatedRoute: ActivatedRoute,
    @Inject(DOCUMENT) private document: Document,
    private sharedService: SharedService,
    private cookieService: CookieService
  ) {}
  ngOnInit(): void {
    this.lang = this.sharedService.lang.value;
    this.cookieService.set('lang', this.sharedService.lang.value);
    this.ChangeCSSFile(this.cookieService.get('lang'));
  }

  switchLang(lang: string) {
    this.htmlTag.dir = lang === 'ar' ? 'rtl' : 'ltr';
    this.htmlTag.lang = lang === 'ar' ? 'ar' : 'en';
    this.lang = this.htmlTag.lang;
    this.sharedService.lang.next(this.lang);
    this.ChangeCSSFile(lang);
    this.translate.use(lang);
    sessionStorage.setItem('lang', this.sharedService.lang.value);
    this.cookieService.set('lang', this.sharedService.lang.value);
  }

  ChangeCSSFile(lang: string) {
    let HeadTag = this.document.getElementsByTagName(
      'head'
    )[0] as HTMLHeadElement;
    let existingLink = this.document.getElementById(
      'lang/CSS'
    ) as HTMLLinkElement;
    let existingCustomLink = this.document.getElementById(
      'lang/CustomCSS'
    ) as HTMLLinkElement;

    let bundlename = lang === 'ar' ? 'arabicCSS.css' : 'englishCSS.css';
    let custombundlename = lang === 'ar' ? 'customAr.css' : 'customEn.css';
    if (existingLink) {
      existingLink.href = bundlename;
      existingCustomLink.href = custombundlename;
    } else {
      let newlink = this.document.createElement('link');
      newlink.rel = 'stylesheet';
      newlink.type = 'text/css';
      newlink.href = bundlename;
      newlink.id = 'lang/CSS';
      HeadTag.appendChild(newlink);
      let newCustomlink = this.document.createElement('link');
      newCustomlink.rel = 'stylesheet';
      newCustomlink.type = 'text/css';
      newCustomlink.href = custombundlename;
      newCustomlink.id = 'lang/CustomCSS';
      HeadTag.appendChild(newCustomlink);
    }
  }

  logout() {
    // sessionStorage.removeItem('token');
    // window.location.href = 'http://localhost:4200/login';
    // this.router.navigate(['/','login']);
  }
}
