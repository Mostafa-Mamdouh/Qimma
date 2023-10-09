import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { TreeTable } from 'primeng/treetable';
import { AccountService } from 'src/services/Account/account.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent  {
  title = 'app';
  urlArray: any;
  showBackground!: boolean;
  lang: any = 'en';
  constructor(
    private route: Router,
    private translate: TranslateService,
    public messageService: MessageService,
    public accountService: AccountService,

    public confirmationService: ConfirmationService,

  ) { }

  get isShowBackground() {
    return this.showBackground;
  }

  

  async ngOnInit(): Promise<void> {
    await this.accountService.loadCurrentUser();
    this.translate.use('en');

    this.route.events.pipe().subscribe((value) => {
      if (value instanceof NavigationEnd) {
        this.urlArray = this.route.url.toString().split('/');
        if (this.urlArray[1] == '') {
          this.showBackground = false;
        } else {
          this.showBackground = true;
        }
      }
    });
  }
}
