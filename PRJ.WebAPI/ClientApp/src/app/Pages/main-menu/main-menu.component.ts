import { Component, OnInit } from '@angular/core';
import { FacilityProfileService } from 'src/app/services/FacilityProfile/facility-profile.service';
import { ManufacturerMasterService } from './../../services/ManufacturerMaster/manufacturer-master.service';

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.css'],
})
export class MainMenuComponent implements OnInit {
  menus: any[] = [
    {
      shortMenuDesFrn: 'Insert New Record',
      shortMenuDesNtv: 'أدخل سجل جديد',
      menuDesFrn: 'Insert new record based on the permit',
      menuDesNtv: 'أدخل سجل جديد بناءً على الفسح',
      path: 'insert-new-record',
    },
    {
      shortMenuDesFrn: 'Update Essential Information',
      shortMenuDesNtv: 'تعديل البيانات الأساسية',
      menuDesFrn: 'update information that require an approval from the NRRC',
      menuDesNtv: 'تعديل البيانات التي تتطلب موافقة الهيئة',
      path: 'page2',
    },
    {
      shortMenuDesFrn: 'Sources View',
      shortMenuDesNtv: 'عرض المصادر',
      menuDesFrn: 'View the sources of the facility',
      menuDesNtv: 'عرض مصادر المنشأة',
      path: 'page3',
    },
    {
      shortMenuDesFrn: 'Update Non-Essential Information',
      shortMenuDesNtv: 'تعديل البيانات غير الأساسية',
      menuDesFrn:
        'update information that dose not require an approval from the NRRC',
      menuDesNtv: 'تعديل البيانات التي تتطلب موافقة الهيئة',
      path: 'page 4',
    },
  ];

  constructor(private service: ManufacturerMasterService) {}

  ngOnInit(): void {}
}
