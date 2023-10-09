import { Component, OnInit, Input, forwardRef, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators,FormArray, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faLock, faUser } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { AppComponent } from 'src/app/app.component';
import UserData from 'src/app/models/Common/userdata';
import { AccountService } from 'src/app/services/Account/account.service';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { Response } from '../../../models/Common/response';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  @Input() menu?: any;
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  loginForm: FormGroup;
  returnUrl: any;
  faUser = faUser;
  faLock = faLock;




  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private _router: Router,private router: ActivatedRoute, private sharedService: SharedService, private accountService: AccountService) { }



  ngOnInit(): void {
// get return url from route parameters or default to '/'
this.returnUrl = this.router.snapshot.queryParams['returnUrl']; // || '/';
if (this.returnUrl != undefined && this.returnUrl != '/')
  this._router.navigateByUrl(this.returnUrl);

    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
    this.createLoginForm();
 }

  createLoginForm() {
    this.loginForm = new FormGroup({
      username: new FormControl('', [
        Validators.required,
      ]),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    var _me = this;
    if (_me.loginForm.valid){
      this.accountService.login(_me.loginForm.value).subscribe(function (data) {
        var response = data as unknown as Response<UserData>;
      if (response.succeeded) {
        if (response.data.username) {
          _me._router.navigateByUrl('/');
        } else {
          _me.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _me.translateService.instant('General.Error'), detail: _me.translateService.instant('General.Error') + " " + 'Invalid Credentials', life: 6000 });
          _me._router.navigateByUrl('/login');
        }
      }
      else {
        _me.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _me.translateService.instant('General.Error'), detail: _me.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
      }
    
    });
    }
    else{
      this.sharedService.markFormGroupTouched(this.loginForm);
    }
  }


 


  action(path: string) {
    this._router.navigate([path]);
  }
}
