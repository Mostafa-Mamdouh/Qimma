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
  selector: 'app-aman-settings',
  templateUrl: './aman-settings.html',
})
export class AmanSettingsComponent implements OnInit {
  menus: any[] = [
    {
      shortMenuDesFrn: 'Entity List',
      shortMenuDesNtv: 'قائمة الممارسات الاشعاية',
      menuDesFrn: 'View Integrated Entity List',
      menuDesNtv: 'قائمة الممارسات الاشعاية',
      path: 'entity',
      imgSrc: '../../../assets/imgs/entity.svg',
    },
    {
      shortMenuDesFrn: 'Facility List',
      shortMenuDesNtv: 'قائمة المنشآت',
      menuDesFrn: 'View Integrated Facility List',
      menuDesNtv: 'قائمة المنشآت',
      path: 'facility',
      imgSrc: '../../../assets/imgs/facility.svg',
    },
    {
      shortMenuDesFrn: 'License List',
      shortMenuDesNtv: 'قائمة الرخص',
      menuDesFrn: 'View License Facility List',
      menuDesNtv: 'قائمة الرخص',
      path: 'license',
      imgSrc: '../../../assets/imgs/license.svg',
    },
    {
      shortMenuDesFrn: 'Practise List',
      shortMenuDesNtv: 'قائمة الممارسات',
      menuDesFrn: 'View Integrated Practise List',
      menuDesNtv: 'قائمة الممارسات',
      path: 'practise-profile',
      imgSrc: '../../../assets/imgs/practise.svg',
    },
    {
      shortMenuDesFrn: 'Legal Representatives List',
      shortMenuDesNtv: 'قائمة الممثلين القانونيين',
      menuDesFrn: 'View Legal Representatives List',
      menuDesNtv: 'الاطلاع على قائمة الممثلين القانونيين',
      path: 'legal',
      imgSrc: '../../../assets/imgs/legalrep.svg',
    },
    {
      shortMenuDesFrn: 'Safety Officer List',
      shortMenuDesNtv: 'قائمة ضابط السلامة',
      menuDesFrn: 'Safety Officer List',
      menuDesNtv: 'قائمة ضابط السلامة',
      path: 'safety',
      imgSrc: '../../../assets/imgs/safetyofficer.svg',
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
