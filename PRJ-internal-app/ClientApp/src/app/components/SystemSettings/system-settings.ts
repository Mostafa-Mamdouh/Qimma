import {
  ChangeDetectorRef,
  Component,
  forwardRef,
  Inject,
  OnInit,
} from '@angular/core';
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
  selector: 'app-system-settings',
  templateUrl: './system-settings.html',
})
export class SystemSettingsComponent implements OnInit {
  menus: any[] = [
    {
      shortMenuDesFrn: 'Radionuclide',
      shortMenuDesNtv: 'المصادر الإشعاعية',
      menuDesFrn: 'Create, update, delete radionuclide',
      menuDesNtv: 'اضافة، تحديث وحذف المصادر الإشعاعية',
      path: 'radionuclide-setup',
      imgSrc: '../../../assets/imgs/radionuclide-newicon.svg',
    },
    {
      shortMenuDesFrn: 'Manufacturers',
      shortMenuDesNtv: 'المصانع',
      menuDesFrn: 'Create, update, delete manufacturers',
      menuDesNtv: 'اضافة، تحديث وحذف المصانع',
      path: 'manufacturer-master',
      imgSrc: '../../../assets/imgs/manufacturer -newicons.svg',
    },
    {
      shortMenuDesFrn: 'Countries / Cites',
      shortMenuDesNtv: 'الدول / المدن',
      menuDesFrn: 'Create, update, delete countries / cites',
      menuDesNtv: 'اضافة، تحديث وحذف الدول والمدن',
      path: 'bas-countries',
      imgSrc: '../../../assets/imgs/country - newicons.svg',
    },
    {
      shortMenuDesFrn: 'System Look Up',
      shortMenuDesNtv: 'قوائم النظام',
      menuDesFrn: 'Create, update, delete system look up',
      menuDesNtv: 'اضافة، تحديث وحذف قوائم النظام',
      path: 'lookup-set',
      imgSrc: '../../../assets/imgs/systemlookup - newicons.svg',
    },
    {
      shortMenuDesFrn: 'Internal System Users',
      shortMenuDesNtv: 'ادارة مستخدمين النظام',
      menuDesFrn: 'Manage Users / Roles and system access for internal users',
      menuDesNtv: 'وصف التصنيف في القائمة الرئيسية لبيان عمل هذه الخانة',
      path: 'internal-user-settings',
      imgSrc: '../../../assets/imgs/internaluser - newicon.svg',
    },
    {
      shortMenuDesFrn: 'External System Users',
      shortMenuDesNtv: 'ادارة مستخدمين النظام',
      menuDesFrn: 'Manage Users / Roles and system access for internal users',
      menuDesNtv: 'وصف التصنيف في القائمة الرئيسية لبيان عمل هذه الخانة',
      path: 'page 5',
      imgSrc: '../../../assets/imgs/externaluser - newicon.svg',
    },

    {
      shortMenuDesFrn: 'Customer Profile',
      shortMenuDesNtv: 'ملف الزائر',
      menuDesFrn: 'Customer Profile',
      menuDesNtv: 'املف الزائر',
      path: 'customer-profile',
      imgSrc: '../../../assets/imgs/legalrep.svg',
    },
    {
      shortMenuDesFrn: 'Workers',
      shortMenuDesNtv: 'العمال',
      menuDesFrn: 'Worker Profile',
      menuDesNtv: 'ملف العمال',
      path: 'workers',
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
    private titleService: Title
  ) {
    this.route.queryParams.subscribe((params) => {});
  }

  ngOnInit(): void {
    this.getUserData();
  }
  getUserData(): void {
    var _this = this;
  }
}
