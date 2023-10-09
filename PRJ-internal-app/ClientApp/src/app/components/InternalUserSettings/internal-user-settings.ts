import { ChangeDetectorRef, Component, forwardRef, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { CommonServices } from '../../../services/Common/common.service';
import { UserInfoServices } from '../../../services/userinfo.service';
import { AppComponent } from '../../app.component';
import { CommonClass } from '../../common/CommonClass';
import { SharedService } from '../../services/shared.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import UserData from '../../../models/Common/userdata';
import { Response } from '../../../models/Common/response';

@Component({
  selector: 'internal-user-settings',
  templateUrl: './internal-user-settings.html',
})
export class InternalUserSettingsComponent implements OnInit {
  menus: any[] = [
    
    
    {
      shortMenuDesFrn: 'Screens Management',
      shortMenuDesNtv: 'ادارة شاشات النظام',
      menuDesFrn:
        'Manage Screens / Create, update, delete Screens',
      menuDesNtv: 'اضافة، تحديث وحذف شاشات النظام',
      path: 'screens',
      imgSrc: '../../../assets/imgs/main/usersw.svg',
    },
    {
      shortMenuDesFrn: 'List Of Values',
      shortMenuDesNtv: 'ادارة عناصر النظام',
      menuDesFrn:
        'Manage Lov / Create, update, delete Screens',
      menuDesNtv: 'اضافة، تحديث وحذف عناصر النظام',
      path: 'lov',
      imgSrc: '../../../assets/imgs/main/usersw.svg',
    },
    {
      shortMenuDesFrn: 'Role Management',
      shortMenuDesNtv: 'ادارة مهام النظام',
      menuDesFrn:
        'Manage Roles / Create, update, delete Roles',
      menuDesNtv: 'اضافة، تحديث وحذف مهام النظام',
      path: 'roles',
      imgSrc: '../../../assets/imgs/main/usersw.svg',
    },
  ];

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private commonServices: CommonServices,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef,
    private sharedService: SharedService,
    private ConfirmationService: ConfirmationService,
    private MessageService: MessageService,
    private commonClass: CommonClass,
    private titleService: Title) {
    this.route.queryParams.subscribe(params => {
      
    });
  }

  ngOnInit(): void {
    this.getUserData();

   
  }
  getUserData(): void {
    var _this = this;

  }
}
