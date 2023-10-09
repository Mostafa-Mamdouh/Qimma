import { Component, Input, Inject, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AppSettings } from './AppSettings';
import { SharedService } from './services/Shared/shared.service';
import { HostListener } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { DOCUMENT, ViewportScroller } from '@angular/common';
import { AccountService } from './services/Account/account.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { InsertNewRecordService } from './services/Insert-New-Recored/insert-new-record.service';
import { faPen } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  windowScrolled: boolean;
  faPen = faPen;

  @HostListener('window:scroll', [])
  onWindowScroll() {
    if (
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop > 100
    ) {
      this.windowScrolled = true;
    } else if (
      (this.windowScrolled && window.pageYOffset) ||
      document.documentElement.scrollTop ||
      document.body.scrollTop < 10
    ) {
      this.windowScrolled = false;
    }
  }
  // scrollToTop() {
  //   (function smoothscroll() {
  //     var currentScroll =
  //       document.documentElement.scrollTop || document.body.scrollTop;
  //     if (currentScroll > 0) {
  //       window.requestAnimationFrame(smoothscroll);
  //       window.scrollTo(0, currentScroll - currentScroll / 8);
  //     }
  //   })();
  // }
  title = 'NRRC-FrontEnd';

  urlArray: any;
  showBackground: boolean;
  lang: any = 'en';
  isCollapsed = true;
  WebApiUrl: string = AppSettings.WebApiUrl;
  htmlTag = this.document.getElementsByTagName('html')[0] as HTMLHtmlElement;

  // pageYoffset = 0;

  // @HostListener('window:scroll', ['$event']) onScroll(event) {
  //   this.pageYoffset = window.pageYOffset;
  // }

  scrollToTop() {
    this.scroll.scrollToPosition([0, 0]);
  }

  get isShowBackground() {
    return this.showBackground;
  }

  constructor(
    private route: Router,
    private translate: TranslateService,
    private shared: SharedService,
    private accountService: AccountService,
    private scroll: ViewportScroller,
    private cookieService: CookieService,
    @Inject(DOCUMENT) private document: Document,
    public messageService: MessageService,
    public confirmationService: ConfirmationService,
    private insertNewRecordService: InsertNewRecordService
  ) {}

  async ngOnInit(): Promise<void> {
    await this.accountService.loadCurrentUser();
    if (this.cookieService.get('lang') != '') {
      this.switchLang(this.cookieService.get('lang'));
    } else {
      this.translate.use('en');
    }

    this.route.events.pipe().subscribe((value) => {
      if (value instanceof NavigationEnd) {
        this.urlArray = this.route.url.toString().split('/');
        if (this.urlArray[1] == 'main') {
          this.showBackground = false;
        } else {
          this.showBackground = true;
        }
      }
    });
  }

  switchLang(lang: string) {
    this.htmlTag.dir = lang === 'ar' ? 'rtl' : 'ltr';
    this.htmlTag.lang = lang === 'ar' ? 'ar' : 'en';
    this.lang = this.htmlTag.lang;
    this.shared.lang.next(this.lang);
    this.ChangeCSSFile(lang);
    this.translate.use(lang);
    sessionStorage.setItem('lang', this.shared.lang.value);
    this.cookieService.set('lang', this.shared.lang.value);
  }

  ChangeCSSFile(lang: string) {
    this.lang = lang;
    console.log(lang);
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
}
