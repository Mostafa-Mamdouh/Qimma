import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  menus: any[] = [
    {
      shortMenuDesFrn: 'Aman Integration Settings / Data',
      shortMenuDesNtv: 'بيانات واعدادات الربط مع نظام امان',
      menuDesFrn:
        'Aman Integration Settings / Data view data and integration settings',
      menuDesNtv: 'بيانات واعدادات الربط مع نظام امان عرض البيانات والربط',
      path: 'aman-settings',
      imgSrc: '../../../assets/imgs/amanintegration.svg',
    },
    {
      shortMenuDesFrn: 'System Settings',
      shortMenuDesNtv: 'اعدادات النظام',
      menuDesFrn: 'System Setting (Domain, Users Settings)',
      menuDesNtv: 'وصف التصنيف في القائمة الرئيسية لبيان عمل هذه الخانة',
      path: 'system-settings',
      imgSrc: '../../../assets/imgs/systemsettings.svg',
    },
    {
      shortMenuDesFrn: 'Action Center',
      shortMenuDesNtv: 'مركز العمليات',
      menuDesFrn: 'Transction sources / Item sources Profile',
      menuDesNtv: 'وصف العمليات داخل النظام',
      path: 'action-center',
      imgSrc: '../../../assets/imgs/actioncenter2.svg',
    },
    {
      shortMenuDesFrn: 'Enquiry Center',
      shortMenuDesNtv: 'مركز الاستعلامات',
      menuDesFrn: 'Transction sources / Item sources Profile',
      menuDesNtv: 'الاستعلام عن العمليات داخل النظام',
      path: 'trn-enquiry',
      imgSrc: '../../../assets/imgs/enquirycenter.svg',
    },
    {
      shortMenuDesFrn: 'Nuclear Materials',
      shortMenuDesNtv: 'المواد النووية',
      menuDesFrn: 'Add, Edit, Delete, View Nuclear Materials',
      menuDesNtv: 'أضف, عدل, أحذف, أستعرض المواد النووية',
      path: 'nuclear-materials',
      imgSrc: '../../../assets/imgs/nuclearmaterial.svg',
    },
    {
      shortMenuDesFrn: 'Billing',
      shortMenuDesNtv: 'الفواتير',
      menuDesFrn: 'Billing',
      menuDesNtv: 'الفواتير',
      path: 'billing',
      imgSrc: '../../../assets/imgs/nuclearmaterial.svg',
    },
    {
      shortMenuDesFrn: 'Billing Transaction Service',
      shortMenuDesNtv: 'خدمة معاملات الفواتير',
      menuDesFrn: 'Billing Transaction Service',
      menuDesNtv: 'خدمة معاملات الفواتير',
      path: 'billing-trn-service',
      imgSrc: '../../../assets/imgs/nuclearmaterial.svg',
    },
    {
      shortMenuDesFrn: 'Related Items',
      shortMenuDesNtv: 'الأصناف المتعلقة',
      menuDesFrn: 'Create, update, delete Related Items',
      menuDesNtv: 'اضافة، تحديث وحذف الأصناف المتعلقة',
      path: 'related-items',
      imgSrc: '../../../assets/imgs/main/radionuc.svg',
    },
    // {
    //   shortMenuDesFrn: 'Menu Items Hierarchy',
    //   shortMenuDesNtv: 'لتسلسل الهرمي لعناصر القائمة',
    //   menuDesFrn: 'Menu Items Hierarchy',
    //   menuDesNtv: 'التسلسل الهرمي لعناصر القائمة',
    //   path: 'menu-items-hierarchy',
    //   imgSrc: '../../../assets/imgs/entity.svg',
    // },
  ];
}
