import { Component, forwardRef, Inject, OnInit } from '@angular/core';
import { CommonServices } from '../../../../services/Common/common.service';
import { AppComponent } from '../../../app.component';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { TranslateService } from '@ngx-translate/core';
import { RadionuclideService } from '../../../../services/Radionuclide/radionuclide.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AttachmentsTypes, SourceTypes } from '../../../../Enumerations/Enums';
import {
  faPlus,
  faXmark,
  faTrash,
  faTrashAlt,
} from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { SharedService } from '../../../services/shared.service';
import { EntityServices } from '../../../../services/Entity/entity.service';
import { NuclearMaterialServices } from '../../../../services/NuclearMaterial/nuclear-material.service';
import { Base64ToPdf, PdfToBase64 } from '../../../../helper/fileToBase64';
import {
  NuclearMaterial,
  NuclearMaterialElements,
  NuclearMaterialFile,
  NuclearMaterialRadionuclide,
} from '../../../../models/NuclearMaterial/NuclearMaterial';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-nuclear-material-form',
  templateUrl: './nuclear-material-form.component.html',
  styleUrls: ['./nuclear-material-form.component.css'],
})
export class NuclearMaterialFormComponent implements OnInit {
  // Language
  lang: string;
  langServiceSupscribtion: Subscription;

  // Lookups and Drop down lists
  lookups?: any = {};
  statusLookup?: any[];
  entities?: any[] = [];
  facilities?: any[] = [];
  licenses?: any[] = [];
  permits?: any[] = [];
  puRadionulcidesList?: any[] = [];
  thRadionulcidesList?: any[] = [];
  duRadionulcidesList?: any[] = [];
  euRadionulcidesList?: any[] = [];
  nuRadionulcidesList?: any[] = [];

  // Icons
  faPlus = faPlus;
  faXmark = faXmark;
  faTrash = faTrash;

  // Enumerations
  SourceTypes = SourceTypes;
  AttachmentsTypes = AttachmentsTypes;

  // Disable Dates after today
  currentDate: any = new Date();

  // Source Variables and Attachments

  // Attachments
  selectedSourceAttachments?: any[] = [];
  selectedMaterialAttachments?: any[] = [];
  sourceFiles: File[] = [];

  isSourceAttachmentValid: boolean = true;

  manufacturerCertificates: File[] = [];
  customImportPermits: File[] = [];
  shipperImportPermits: File[] = [];
  customExportPermits: File[] = [];
  shipperExportPermits: File[] = [];
  otherDocumnets: File[] = [];
  images: File[] = [];
  charCertificates: File[] = [];
  sourceTagImages: File[] = [];

  isSourceManufacturerCertificateValid: boolean = true;
  isSourceCustomImportPermitsValid: boolean = true;
  isSourceShipperImportPermitsValid: boolean = true;
  isImagesValid: boolean = true;
  isCharCertificateValid: boolean = true;
  isSourceTagImagesValid: boolean = true;

  isManufacturerCertificate: boolean = true;
  isCharCertificate: boolean = false;
  isCustomImportPermit: boolean = true;
  isShipperImportPermit: boolean = true;
  isImage: boolean = true;
  isSourceTagImage = false;

  // Is Uranium
  IsUranium = false;

  // Is Uranium Enriched
  IsUraniumEnriched = false;

  isDU = false;
  isEU = false;
  isNU = false;
  isPu = false;
  isTh = false;

  // Form Validation Variables
  submitBtnClicked: boolean = false;

  //Update
  updateId: string = null;
  updateObj: any;

  //view
  isView: boolean = false;

  // Form
  addNewSourceForm = this.fb.group({
    sourceId: [''],
    manufacturerSerialNo: ['', Validators.required],
    sourceModel: ['', Validators.required],
    facilitySerialNo: [''],
    elements: this.fb.array([
      this.fb.group({
        id: [null],
        nuclearMaterialType: [null, Validators.required],
        elementMass: [null, Validators.required],
        elementMassUnit: [null, Validators.required],
      }),
    ]),
    quantity: [0, Validators.required],
    quantityUnitId: ['', Validators.required],
    chemicalForm: [''],
    isShield: [false, Validators.required],
    status: ['', Validators.required],
    physicalForm: ['', Validators.required],
    entityId: ['', Validators.required],
    facilityId: ['', Validators.required],
    licenseId: ['', Validators.required],
    permitdetailsId: ['', Validators.required],
    manufacturerId: ['', Validators.required],
    manufacturerCountryId: ['', Validators.required],
    PuRadionulcides: this.fb.array([]),
    ThRadionulcides: this.fb.array([]),
    DURadionulcides: this.fb.array([]),
    NURadionulcides: this.fb.array([]),
    EURadionulcides: this.fb.array([]),
    nuclearMaterialFiles: this.fb.array([]),
    noManufacturerCertificateFlag: [false, Validators.required],
    noCustomImportPermitFlag: [false, Validators.required],
    noShipperImportPermitFlag: [false, Validators.required],
    noImagesFlag: [false, Validators.required],
    noCharacterizationCertificateFlag: [false, Validators.required],
    noSourceTagImageFlag: [false, Validators.required],
  });

  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private fb: FormBuilder,
    private commonServices: CommonServices,
    private confirmationService: ConfirmationService,
    private translateService: TranslateService,
    private radionuclideService: RadionuclideService,
    private sharedService: SharedService,
    private entityServices: EntityServices,
    private nuclearMaterialServices: NuclearMaterialServices,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.route.queryParams.subscribe((params) => {
      this.updateId = params['id'];
      this.isView = params['isView'];
    });
  }

  ngOnInit(): void {
    // get language
    this.langServiceSupscribtion = this.sharedService.lang.subscribe((l) => {
      this.lang = l;
    });

    this.commonServices.getSourceFormsLookup().subscribe((res) => {
      res['data'].forEach((element) => {
        this.lookups[element['className']] = element['lookupSetTerms'];
        console.log(this.lookups);
      });
    });

    this.entityServices.GetAll().subscribe((res) => {
      this.entities = res['data'];
    });

    if (this.updateId) {
      this.nuclearMaterialServices
        .getNuclearMaterialById(this.updateId)
        .subscribe((res) => {
          if (res['succeeded']) {
            this.updateObj = res['data'];
            console.log('ById: ', this.updateObj);
            this.setUpdateData();
          }
        });
    }
  }

  // get PuRadionulcides
  get PuRadionulcides() {
    return this.addNewSourceForm.get('PuRadionulcides') as FormArray;
  }

  // add PuRadionulcides
  addPuRadionulcides() {
    this.PuRadionulcides.push(
      this.fb.group({
        nuclearMaterialRadionulcideId: [null],
        element: ['pu'],
        enrichment: [null],
        radionulcideId: ['', Validators.required],
        materialId: [null],
      })
    );
  }

  // remove PuRadionulcides
  removePuRadionulcides(index: number) {
    this.PuRadionulcides.removeAt(index);
  }
  // get ThRadionulcides
  get ThRadionulcides() {
    return this.addNewSourceForm.get('ThRadionulcides') as FormArray;
  }

  // add ThRadionulcides
  addThRadionulcides() {
    this.ThRadionulcides.push(
      this.fb.group({
        nuclearMaterialRadionulcideId: [null],
        element: ['th'],
        enrichment: [null],
        radionulcideId: ['', Validators.required],
        materialId: [null],
      })
    );
  }

  // remove ThRadionulcides
  removeThRadionulcides(index: number) {
    this.ThRadionulcides.removeAt(index);
  }

  // get DURadionulcides
  get DURadionulcides() {
    return this.addNewSourceForm.get('DURadionulcides') as FormArray;
  }

  addDURadionulcides() {
    this.DURadionulcides.push(
      this.fb.group({
        nuclearMaterialRadionulcideId: [null],
        enrichment: [''],
        element: ['d'],
        radionulcideId: ['', Validators.required],
        materialId: [null],
      })
    );
  }

  removeDURadionulcides(i: number) {
    this.DURadionulcides.removeAt(i);
  }

  // get NURadionucides
  get NURadionulcides() {
    return this.addNewSourceForm.get('NURadionulcides') as FormArray;
  }

  addNURadionulcides() {
    this.NURadionulcides.push(
      this.fb.group({
        nuclearMaterialRadionulcideId: [null],
        enrichment: [''],
        element: ['n'],
        radionulcideId: [''],
        materialId: [null],
      })
    );
  }

  removeNURadionulcides(i: number) {
    this.NURadionulcides.removeAt(i);
  }

  // get EURadionucides
  get EURadionulcides() {
    return this.addNewSourceForm.get('EURadionulcides') as FormArray;
  }

  addEURadionulcides() {
    this.EURadionulcides.push(
      this.fb.group({
        nuclearMaterialRadionulcideId: [null],
        enrichment: [''],
        element: ['e'],
        radionulcideId: ['', Validators.required],
        materialId: [null],
      })
    );
  }

  removeEURadionulcides(i: number) {
    this.EURadionulcides.removeAt(i);
  }

  // Material Isoptops and enrichment
  addDURadionulcidesAndEnrichment(nuclearMaterial) {
    this.DURadionulcides.clear();
    this.duRadionulcidesList.forEach((element, i) => {
      this.DURadionulcides.push(
        this.fb.group({
          nuclearMaterialRadionulcideId: [null],
          enrichment: [
            this.updateObj?.nuclearMaterialRadionulcides[i].enrichment,
            Validators.required,
          ],
          element: [nuclearMaterial],
          radionulcideId: [
            this.updateObj
              ? this.updateObj?.nuclearMaterialRadionulcides[i].radionulcideId
              : element.id,
            Validators.required,
          ],
          materialId: [null],
        })
      );
    });
  }

  addnNURadionulcidesAndEnrichment(nuclearMaterial) {
    this.NURadionulcides.clear();
    this.nuRadionulcidesList.forEach((element, i) => {
      this.NURadionulcides.push(
        this.fb.group({
          nuclearMaterialRadionulcideId: [null],
          enrichment: [
            this.updateObj?.nuclearMaterialRadionulcides[i].enrichment,
            Validators.required,
          ],
          element: [nuclearMaterial],
          radionulcideId: [
            this.updateObj
              ? this.updateObj?.nuclearMaterialRadionulcides[i].radionulcideId
              : element.id,
            Validators.required,
          ],
          materialId: [null],
        })
      );
    });
  }

  addEURadionulcidesAndEnrichment(nuclearMaterial) {
    this.EURadionulcides.clear();
    this.euRadionulcidesList.forEach((element, i) => {
      this.EURadionulcides.push(
        this.fb.group({
          nuclearMaterialRadionulcideId: [null],
          enrichment: [
            this.updateObj?.nuclearMaterialRadionulcides[i].enrichment,
            Validators.required,
          ],
          element: [nuclearMaterial],
          radionulcideId: [
            this.updateObj
              ? this.updateObj?.nuclearMaterialRadionulcides[i].radionulcideId
              : element.id,
            Validators.required,
          ],
          materialId: [null],
        })
      );
    });
  }
  // get elements
  get elements() {
    return this.addNewSourceForm.get('elements') as FormArray;
  }

  addElements() {
    this.elements.push(
      this.fb.group({
        id: [null],
        nuclearMaterialType: [null, Validators.required],
        elementMass: [null, Validators.required],
        elementMassUnit: [null, Validators.required],
      })
    );
  }

  removeElements(i: number) {
    let type = this.elements.at(i).value.nuclearMaterialType?.value;
    let x = this.lookups['NuclearMaterial'].find((x) => x.value == type);
    if (x) {
      x.extraData1 = false;
    }
    this.elements.removeAt(i);
    this.onElementChangeOrDelete();
  }

  // Source Attachments FormArray Methods
  get sourceAttachments() {
    return this.addNewSourceForm.get('nuclearMaterialFiles') as FormArray;
  }

  addSourceAttachments(file, attachmentType) {
    let StrBase64;

    PdfToBase64(file)
      .then((res) => (StrBase64 = res))
      .then((res) => {
        return {
          fileSourceID: '',
          name: file.name,
          size: file.size,
          base64: StrBase64.toString(),
          contentType: file.type,
          attachmentType: attachmentType,
        };
      })
      .then((res) => {
        this.sourceAttachments.push(this.fb.group(res));
      });
  }

  removeSourceAttachment(i: number) {
    this.sourceAttachments.removeAt(i);
  }

  // Get Facilities by Entity
  getFacilitiesByEntityId(id: string, updateIds?: number[]) {
    this.nuclearMaterialServices.getFacilities(id).subscribe((res) => {
      this.facilities = res['data'];
      if (this.updateObj) {
        let facility = this.facilities.find(
          (x) => x.facilityId == updateIds[0]
        );
        this.addNewSourceForm.controls['facilityId'].setValue(facility);
        this.getLicensesByFacilityId(facility.id, updateIds);
      }
    });
  }
  // Get Licenses by Facility
  getLicensesByFacilityId(id: string, updateIds?: number[]) {
    this.nuclearMaterialServices.getLicenses(id).subscribe((res) => {
      this.licenses = res['data'];
      if (this.updateObj) {
        let license = this.licenses.find((x) => x.licenseId == updateIds[1]);
        this.addNewSourceForm.controls['licenseId'].setValue(license);
        this.getPermitsByLicenseId(license.id, updateIds);
      }
    });
  }
  // Get Permits by License
  getPermitsByLicenseId(id: string, updateIds?: number[]) {
    this.nuclearMaterialServices.getPermits(id).subscribe((res) => {
      this.permits = res['data'];
      if (this.updateObj) {
        let permit = this.permits.find(
          (x) => x.permitDetailsId == updateIds[2]
        );
        this.addNewSourceForm.controls['permitdetailsId'].setValue(permit);
      }
    });
  }

  onElementChangeOrDelete() {
    if (this.elements.value.length > 0) {
      this.isPu = this.elements.value.find(
        (x) => x.nuclearMaterialType.value == 'pu'
      )
        ? true
        : false;
      !this.isPu ? this.PuRadionulcides.clear() : null;

      this.isTh = this.elements.value.find(
        (x) => x.nuclearMaterialType.value == 'th'
      )
        ? true
        : false;
      !this.isTh ? this.ThRadionulcides.clear() : null;

      this.isDU = this.elements.value.find(
        (x) => x.nuclearMaterialType.value == 'd'
      )
        ? true
        : false;
      !this.isDU ? this.DURadionulcides.clear() : null;

      this.isNU = this.elements.value.find(
        (x) => x.nuclearMaterialType.value == 'n'
      )
        ? true
        : false;
      !this.isNU ? this.NURadionulcides.clear() : null;

      this.isEU = this.elements.value.find(
        (x) => x.nuclearMaterialType.value == 'e'
      )
        ? true
        : false;
      !this.isEU ? this.EURadionulcides.clear() : null;
    }
  }

  handleElementChange() {
    this.lookups['NuclearMaterial'].forEach((item) => {
      item.extraData1 = false;
      if (this.elements.value.length > 0) {
        let element = this.elements.value.find(
          (x) => x.nuclearMaterialType.value == item.value
        );
        if (element) {
          if (element.nuclearMaterialType.value == item.value) {
            item.extraData1 = true;
          }
        }
      }
    });

    this.onElementChangeOrDelete();
  }

  private check_if_element_has_radionuclides_from_update(nm: string) {
    if (this.updateObj.nuclearMaterialRadionulcides.length > 0) {
      let hasRads = this.updateObj.nuclearMaterialRadionulcides.find(
        (x) => x.element == nm
      );
      if (hasRads == null || hasRads == -1) {
        return false;
      }
    }
    return true;
  }

  // Get Radionuclides by Nuclear Material
  getRadionuclidesByNuclearMaterial(nuclearMaterial: string) {
    let x = this.lookups['NuclearMaterial'].find(
      (x) => x.value == nuclearMaterial
    );
    if (x) {
      x.extraData1 = true;
    }
    this.handleElementChange();

    this.nuclearMaterialServices
      .getRadionulicdesByNuclearMaterial(nuclearMaterial)
      .subscribe((res) => {
        if (nuclearMaterial == 'pu') {
          this.puRadionulcidesList = res['data'];
          this.isPu = true;
          if (
            !this.updateObj ||
            !this.check_if_element_has_radionuclides_from_update('pu')
          ) {
            this.addPuRadionulcides();
          }
        } else if (nuclearMaterial == 'th') {
          this.thRadionulcidesList = res['data'];
          this.isTh = true;
          if (
            !this.updateObj ||
            !this.check_if_element_has_radionuclides_from_update('th')
          ) {
            this.addThRadionulcides();
          }
        } else if (nuclearMaterial == 'd') {
          this.duRadionulcidesList = res['data'];
          this.isDU = true;
          if (
            !this.updateObj ||
            !this.check_if_element_has_radionuclides_from_update('d')
          ) {
            this.addDURadionulcidesAndEnrichment(nuclearMaterial);
          }
        } else if (nuclearMaterial == 'e') {
          this.euRadionulcidesList = res['data'];
          this.isEU = true;
          if (
            !this.updateObj ||
            !this.check_if_element_has_radionuclides_from_update('e')
          ) {
            this.addEURadionulcidesAndEnrichment(nuclearMaterial);
          }
        } else if (nuclearMaterial == 'n') {
          this.nuRadionulcidesList = res['data'];
          this.isNU = true;
          if (
            !this.updateObj ||
            !this.check_if_element_has_radionuclides_from_update('n')
          ) {
            this.addnNURadionulcidesAndEnrichment(nuclearMaterial);
          }
        }
        if (this.updateObj) {
          this.renderRadionuclidesOnUpdate(nuclearMaterial);
        }
      });
  }

  // Alert if the user try to select radionuclide before selecting nuclear material
  alertIfNuclearMaterialNotSelected() {
    if (!this.addNewSourceForm.get('nuclearMaterial').value) {
      this.app.messageService.add({
        severity: 'warn',
        key: 'PlanValidation',
        summary: 'Error',
        detail: this.translateService.instant(
          'addNewSource.selectNuclearMaterialToSelectIsotop'
        ),
        life: 6000,
      });
    }
  }

  // Attachments Methods
  onSelect(event, field) {
    this.sourceFiles = [];
    this.sourceFiles.push(...event.addedFiles);
    for (let i = 0; i < this.sourceFiles.length; i++) {
      this.addSourceAttachments(<File>this.sourceFiles[i], field);

      if (field == AttachmentsTypes.manufacturerCertificate)
        this.manufacturerCertificates.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.customImportPermits)
        this.customImportPermits.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.images)
        this.images.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.shipperImportPermits)
        this.shipperImportPermits.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.charCertificates)
        this.charCertificates.push(this.sourceFiles[i]);
      else if (field == AttachmentsTypes.sourceTagImage)
        this.sourceTagImages.push(this.sourceFiles[i]);
      else this.otherDocumnets.push(this.sourceFiles[i]);
    }
  }

  onRemove(event, field) {
    this.sourceFiles = [];

    if (field == AttachmentsTypes.manufacturerCertificate) {
      this.manufacturerCertificates.splice(
        this.manufacturerCertificates.indexOf(event),
        1
      );
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else if (field == AttachmentsTypes.customImportPermits) {
      this.customImportPermits.splice(
        this.customImportPermits.indexOf(event),
        1
      );
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else if (field == AttachmentsTypes.images) {
      this.images.splice(this.images.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else if (field == AttachmentsTypes.shipperImportPermits) {
      this.shipperImportPermits.splice(
        this.shipperImportPermits.indexOf(event),
        1
      );
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else if (field == AttachmentsTypes.charCertificates) {
      this.charCertificates.splice(this.charCertificates.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else if (field == AttachmentsTypes.sourceTagImage) {
      this.sourceTagImages.splice(this.sourceTagImages.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    } else {
      this.otherDocumnets.splice(this.otherDocumnets.indexOf(event), 1);
      this.sourceFiles.push(...this.manufacturerCertificates);
      this.sourceFiles.push(...this.customImportPermits);
      this.sourceFiles.push(...this.images);
      this.sourceFiles.push(...this.shipperImportPermits);
      this.sourceFiles.push(...this.otherDocumnets);
      this.sourceFiles.push(...this.charCertificates);
      this.sourceFiles.push(...this.sourceTagImages);
    }

    this.addNewSourceForm.controls['nuclearMaterialFiles'].clear();

    for (let i = 0; i < this.manufacturerCertificates.length; i++) {
      this.addSourceAttachments(
        <File>this.manufacturerCertificates[i],
        AttachmentsTypes.manufacturerCertificate
      );
    }
    for (let i = 0; i < this.customImportPermits.length; i++) {
      this.addSourceAttachments(
        <File>this.customImportPermits[i],
        AttachmentsTypes.customImportPermits
      );
    }
    for (let i = 0; i < this.images.length; i++) {
      this.addSourceAttachments(<File>this.images[i], AttachmentsTypes.images);
    }
    for (let i = 0; i < this.shipperImportPermits.length; i++) {
      this.addSourceAttachments(
        <File>this.shipperImportPermits[i],
        AttachmentsTypes.shipperImportPermits
      );
    }
    for (let i = 0; i < this.otherDocumnets.length; i++) {
      this.addSourceAttachments(
        <File>this.otherDocumnets[i],
        AttachmentsTypes.otherDocumnets
      );
    }

    for (let i = 0; i < this.sourceTagImages.length; i++) {
      this.addSourceAttachments(
        <File>this.sourceTagImages[i],
        AttachmentsTypes.sourceTagImage
      );
    }

    for (let i = 0; i < this.charCertificates.length; i++) {
      this.addSourceAttachments(
        <File>this.charCertificates[i],
        AttachmentsTypes.charCertificates
      );
    }
  }

  validateAttachments(): boolean {
    let list = this.addNewSourceForm.controls['nuclearMaterialFiles'].value;
    if (this.isManufacturerCertificate)
      this.isSourceManufacturerCertificateValid =
        this.manufacturerCertificates.length == 0 ? false : true;
    else this.isSourceManufacturerCertificateValid = true;

    if (this.isCharCertificate)
      this.isCharCertificateValid =
        this.charCertificates.length == 0 ? false : true;
    else this.isCharCertificateValid = true;

    if (this.isCustomImportPermit) {
      this.isSourceCustomImportPermitsValid =
        this.customImportPermits.length == 0 ? false : true;
    } else this.isSourceCustomImportPermitsValid = true;

    if (this.isShipperImportPermit) {
      this.isSourceShipperImportPermitsValid =
        this.shipperImportPermits.length == 0 ? false : true;
    } else this.isSourceShipperImportPermitsValid = true;

    if (this.isImage) {
      this.isImagesValid = this.images.length == 0 ? false : true;
    } else this.isImagesValid = true;

    if (this.isSourceTagImage) {
      this.isSourceTagImagesValid =
        this.sourceTagImages.length == 0 ? false : true;
    } else this.isSourceTagImagesValid = true;
    if (
      this.isSourceManufacturerCertificateValid &&
      this.isCharCertificateValid &&
      this.isSourceCustomImportPermitsValid &&
      this.isSourceShipperImportPermitsValid &&
      this.isImagesValid &&
      this.isSourceTagImagesValid
    ) {
      this.addNewSourceForm.controls['nuclearMaterialFiles'].setErrors(null);
      this.isSourceAttachmentValid = true;
      return true;
    }

    return false;
  }

  handleCertificateChange(choice) {
    let noManufacturerCertificate =
      this.addNewSourceForm.controls['noManufacturerCertificateFlag'];
    let noCharCertificate =
      this.addNewSourceForm.controls['noCharacterizationCertificateFlag'];
    let noSourceTag = this.addNewSourceForm.controls['noSourceTagImageFlag'];
    let noCustomImport =
      this.addNewSourceForm.controls['noCustomImportPermitFlag'];
    let noShipperImportPermit =
      this.addNewSourceForm.controls['noShipperImportPermitFlag'];
    let imagesFlag = this.addNewSourceForm.controls['noImagesFlag'];

    if (choice == 1) {
      this.isManufacturerCertificate = !noManufacturerCertificate.value;
      this.isCharCertificate = !this.isManufacturerCertificate;
      this.isSourceTagImage = false;
      noCharCertificate.setValue(this.isManufacturerCertificate);
      noSourceTag.setValue(this.isManufacturerCertificate);
    } else if (choice == 2) {
      this.isCharCertificate = !noCharCertificate.value;
      this.isSourceTagImage = !this.isCharCertificate;
      this.isManufacturerCertificate = false;
      noManufacturerCertificate.setValue(true);
      noSourceTag.setValue(this.isCharCertificate);
    } else if (choice == 3) {
      this.isCustomImportPermit = !noCustomImport.value;
    } else if (choice == 4) {
      this.isShipperImportPermit = !noShipperImportPermit.value;
    } else if (choice == 5) {
      this.isImage = !imagesFlag.value;
    } else if (choice == 6) {
      this.isSourceTagImage = !noSourceTag.value;
    }
  }

  renderElementsOnUpdate() {
    if (this.updateObj.nuclearMaterialElements.length > 0) {
      this.elements.clear();
      this.updateObj.nuclearMaterialElements.forEach((element) => {
        let type = element.nuclearMaterialType
          ? this.lookups['NuclearMaterial'].find(
              (x) => x.id == element.nuclearMaterialType
            )
          : null;
        let unit = element.elementMassUnit
          ? this.lookups['WeightUnit'].find(
              (x) => x.id == element.elementMassUnit
            )
          : null;
        this.elements.push(
          this.fb.group({
            id: [element.id],
            nuclearMaterialType: [type, Validators.required],
            elementMass: [element.elementMass, Validators.required],
            elementMassUnit: [unit, Validators.required],
          })
        );
        this.getRadionuclidesByNuclearMaterial(type.value);
        this.handleElementChange();
      });
    }
  }

  renderRadionuclidesOnUpdate(nm) {
    if (
      this.updateObj.nuclearMaterialElements.length > 0 &&
      this.updateObj.nuclearMaterialRadionulcides.length > 0
    ) {
      this.updateObj.nuclearMaterialRadionulcides.forEach((radionulcide) => {
        console.log('radionulcide: ', radionulcide);
        if (nm == 'th' && nm == radionulcide.element) {
          console.log('******');
          let rad = this.thRadionulcidesList.find(
            (x) => x.id == radionulcide.radionulcideId
          );
          this.ThRadionulcides.push(
            this.fb.group({
              nuclearMaterialRadionulcideId: [
                radionulcide.nuclearMaterialRadionulcideId,
              ],
              element: ['th'],
              enrichment: [null],
              radionulcideId: [rad, Validators.required],
              materialId: [radionulcide.materialId],
            })
          );
        } else if (nm == 'pu' && nm == radionulcide.element) {
          let rad = this.puRadionulcidesList.find(
            (x) => x.id == radionulcide.radionulcideId
          );
          this.PuRadionulcides.push(
            this.fb.group({
              nuclearMaterialRadionulcideId: [
                radionulcide.nuclearMaterialRadionulcideId,
              ],
              element: ['pu'],
              enrichment: [null],
              radionulcideId: [rad, Validators.required],
              materialId: [radionulcide.materialId],
            })
          );
        } else if (nm == 'd' && nm == radionulcide.element) {
          this.DURadionulcides.push(
            this.fb.group({
              nuclearMaterialRadionulcideId: [
                radionulcide.nuclearMaterialRadionulcideId,
              ],
              element: ['d'],
              enrichment: [
                { value: radionulcide.enrichment, disabled: this.isView },
              ],
              radionulcideId: [radionulcide.radionulcideId],
              materialId: [radionulcide.materialId],
            })
          );
        } else if (nm == 'n' && nm == radionulcide.element) {
          this.NURadionulcides.push(
            this.fb.group({
              nuclearMaterialRadionulcideId: [
                radionulcide.nuclearMaterialRadionulcideId,
              ],
              element: ['n'],
              enrichment: [
                { value: radionulcide.enrichment, disabled: this.isView },
              ],
              radionulcideId: [
                radionulcide.radionulcideId,
                Validators.required,
              ],
              materialId: [radionulcide.materialId],
            })
          );
        } else if (nm == 'e' && nm == radionulcide.element) {
          this.EURadionulcides.push(
            this.fb.group({
              nuclearMaterialRadionulcideId: [
                radionulcide.nuclearMaterialRadionulcideId,
              ],
              element: ['e'],
              enrichment: [
                { value: radionulcide.enrichment, disabled: this.isView },
              ],
              radionulcideId: [
                radionulcide.radionulcideId,
                Validators.required,
              ],
              materialId: [radionulcide.materialId],
            })
          );
        }
      });
    }
  }

  onChangePhysicalFormChangeUnit() {
    let ph = this.addNewSourceForm.controls['physicalForm'].value;
    console.log('value: ', ph);
    let unit = this.lookups['QuantityUnits'].find(
      (x) => x.extraData1 == ph['value'] || x.extraData2 == ph['value']
    );
    this.addNewSourceForm.controls['quantityUnitId'].setValue(unit);
  }

  setUpdateData() {
    let manufacturer = this.updateObj.manufacturerId
      ? this.lookups['Manufacturer'].find(
          (x) => x.lookupSetTermId == this.updateObj.manufacturerId
        )
      : null;

    let status = this.updateObj.status
      ? this.lookups['Status'].find(
          (x) => x.lookupSetTermId == this.updateObj.status
        )
      : null;

    let unit = this.updateObj.quantityUnitId
      ? this.lookups['QuantityUnits'].find(
          (x) => x.lookupSetTermId == this.updateObj.quantityUnitId
        )
      : null;

    let pf = this.updateObj.physicalForm
      ? this.lookups['PhysicalForm'].find(
          (x) => x.lookupSetTermId == this.updateObj.physicalForm
        )
      : null;

    let mfCountry = this.updateObj.manufacturerCountryId
      ? this.lookups['ManufacturerCountry'].find(
          (x) => x.lookupSetTermId == this.updateObj.manufacturerCountryId
        )
      : null;

    if (this.updateObj?.entityId)
      this.getFacilitiesByEntityId(this.updateObj.entityProfile.id, [
        this.updateObj.facilityId,
        this.updateObj.licenseId,
        this.updateObj.permitdetailsId,
      ]);

    if (this.updateObj.nuclearMaterialFiles.length > 0) {
      this.updateObj.nuclearMaterialFiles?.map((attachment) => {
        console.log(attachment);
        let file = Base64ToPdf(attachment.base64, attachment.name);
        let obj = {
          attachmentType: attachment.attachmentType.toString(),
          base64: attachment.base64,
          contentType: attachment.contentType,
          fileSourceID: null,
          name: attachment.name,
          size: file.size,
        };
        this.sourceAttachments.push(this.fb.group(obj));

        if (
          attachment.attachmentType == AttachmentsTypes.manufacturerCertificate
        ) {
          this.manufacturerCertificates.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.customImportPermits
        ) {
          this.customImportPermits.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.shipperImportPermits
        ) {
          this.shipperImportPermits.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.charCertificates
        ) {
          this.charCertificates.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.otherDocumnets
        ) {
          this.otherDocumnets.push(file);
        } else if (attachment.attachmentType == AttachmentsTypes.images) {
          this.images.push(file);
        } else if (
          attachment.attachmentType == AttachmentsTypes.sourceTagImage
        ) {
          this.sourceTagImages.push(file);
        }
      });
    }

    this.addNewSourceForm.controls['nuclearMaterialFiles'].setValue(
      this.sourceAttachments.value
    );

    this.renderElementsOnUpdate();

    this.addNewSourceForm.patchValue({
      entityId: this.updateObj.entityProfile,
      facilityId: this.updateObj.facilityProfile,
      sourceId: this.updateId,
      manufacturerSerialNo: this.updateObj.manufacturerSerialNo,
      facilitySerialNo: this.updateObj.facilitySerialNo,
      chemicalForm: this.updateObj.chemicalForm,
      isShield: this.updateObj.isShield,
      status: status,
      physicalForm: pf,
      manufacturerId: manufacturer,
      sourceModel: this.updateObj.sourceModel,
      manufacturerCountryId: mfCountry,
      quantity: this.updateObj.quantity,
      quantityUnitId: unit,
      noManufacturerCertificateFlag:
        this.updateObj.noManufacturerCertificateFlag,
      noCustomImportPermitFlag: this.updateObj.noCustomImportPermitFlag,
      noShipperImportPermitFlag: this.updateObj.noShipperImportPermitFlag,
      noImagesFlag: this.updateObj.noImagesFlag,
      noCharacterizationCertificateFlag:
        this.updateObj.noCharacterizationCertificateFlag,
      noSourceTagImageFlag: this.updateObj.noSourceTagImageFlag,
    });
    this.isCustomImportPermit = !this.updateObj.noCustomImportPermitFlag;
    this.isShipperImportPermit = !this.updateObj.noShipperImportPermitFlag;
    this.isImage = !this.updateObj.noImagesFlag;
    this.isSourceTagImage = !this.updateObj.noSourceTagImageFlag;
    this.isCharCertificate = !this.updateObj.noCharacterizationCertificateFlag;
    this.isManufacturerCertificate =
      !this.updateObj.noManufacturerCertificateFlag;

    if (this.isManufacturerCertificate) {
      this.isSourceTagImage = false;
      this.isCharCertificate = false;
    }

    if (this.isCharCertificate) {
      this.isSourceTagImage = false;
      this.isManufacturerCertificate = false;
    }

    if (this.isSourceTagImage) {
      this.isCharCertificate = false;
      this.isManufacturerCertificate = false;
    }

    if (this.isView) {
      this.addNewSourceForm.disable();
    }
  }

  submitNewSource() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth',
    });
    this.submitBtnClicked = true;
    let attachmentsStatus = this.validateAttachments();
    let body = this.addNewSourceForm.value;

    let attachments =
      this.addNewSourceForm.controls['nuclearMaterialFiles'].value;
    let files: NuclearMaterialFile[] = attachments.map((att) => {
      let obj: NuclearMaterialFile = {
        fileId: att['fileSourceID'],
        name: att['name'],
        size: att['size'],
        base64: att['base64'],
        contentType: att['contentType'],
        attachmentType: att['attachmentType'],
        nuclearMaterialId: this.updateId ? this.updateId : null,
      };
      return obj;
    });

    let nuclearMaterialElements: NuclearMaterialElements[] =
      this.elements.value.map((ele) => {
        let nuclearMaterialRadionulcides;
        if (ele.nuclearMaterialType.value == 'pu') {
          nuclearMaterialRadionulcides = this.PuRadionulcides.value.map(
            (item) => {
              let obj: NuclearMaterialRadionuclide = {
                nuclearMaterialRadionulcideId:
                  item['nuclearMaterialRadionulcideId'],
                enrichment: item['enrichment'],
                radionulcideId: item['radionulcideId'].id,
                materialId: item['materialId'],
                element: item['element'],
              };
              return obj;
            }
          );
        }
        if (ele.nuclearMaterialType.value == 'th') {
          nuclearMaterialRadionulcides = this.ThRadionulcides.value.map(
            (item) => {
              let obj: NuclearMaterialRadionuclide = {
                nuclearMaterialRadionulcideId:
                  item['nuclearMaterialRadionulcideId'],
                enrichment: item['enrichment'],
                radionulcideId: item['radionulcideId'].id,
                materialId: item['materialId'],
                element: item['element'],
              };
              return obj;
            }
          );
        }
        if (ele.nuclearMaterialType.value == 'd') {
          nuclearMaterialRadionulcides = this.DURadionulcides.value.map(
            (item) => {
              let obj: NuclearMaterialRadionuclide = {
                nuclearMaterialRadionulcideId:
                  item['nuclearMaterialRadionulcideId'],
                enrichment: item['enrichment'],
                radionulcideId: item['radionulcideId'],
                materialId: item['materialId'],
                element: item['element'],
              };
              return obj;
            }
          );
        }
        if (ele.nuclearMaterialType.value == 'n') {
          nuclearMaterialRadionulcides = this.NURadionulcides.value.map(
            (item) => {
              let obj: NuclearMaterialRadionuclide = {
                nuclearMaterialRadionulcideId:
                  item['nuclearMaterialRadionulcideId'],
                enrichment: item['enrichment'],
                radionulcideId: item['radionulcideId'],
                materialId: item['materialId'],
                element: item['element'],
              };
              return obj;
            }
          );
        }
        if (ele.nuclearMaterialType.value == 'e') {
          nuclearMaterialRadionulcides = this.EURadionulcides.value.map(
            (item) => {
              let obj: NuclearMaterialRadionuclide = {
                nuclearMaterialRadionulcideId:
                  item['nuclearMaterialRadionulcideId'],
                enrichment: item['enrichment'],
                radionulcideId: item['radionulcideId'],
                materialId: item['materialId'],
                element: item['element'],
              };
              return obj;
            }
          );
        }
        let obj: NuclearMaterialElements = {
          id: ele.id,
          elementMass: ele.elementMass,
          elementMassUnit: ele.elementMassUnit?.id,
          nuclearMaterialType: ele.nuclearMaterialType?.id,
          nuclearMaterialId: ele.nuclearMaterialId,
          nuclearMaterialRadionulcides: nuclearMaterialRadionulcides,
        };

        return obj;
      });

    let load: NuclearMaterial = {
      sourceId: body.sourceId,
      nrrcId: this.updateObj ? this.updateObj?.nrrcId : null,
      manufacturerSerialNo: body.manufacturerSerialNo,
      facilitySerialNo: body.facilitySerialNo,
      chemicalForm: body.chemicalForm,
      isShield: body.isShield,
      sourceModel: body.sourceModel,
      quantity: body.quantity,
      quantityUnitId: body.quantityUnitId['id'],
      status: body.status ? body.status['id'] : '',
      physicalForm: body.physicalForm ? body.physicalForm['id'] : '',
      entityId: body.entityId ? body.entityId['id'] : '',
      facilityId: body.facilityId ? body.facilityId['id'] : '',
      licenseId: body.licenseId ? body.licenseId['id'] : '',
      permitdetailsId: body.permitdetailsId ? body.permitdetailsId['id'] : '',
      manufacturerId: body.manufacturerId ? body.manufacturerId['id'] : '',
      manufacturerCountryId: body.manufacturerCountryId
        ? body.manufacturerCountryId['id']
        : '',
      nuclearMaterialElements: nuclearMaterialElements,
      nuclearMaterialFiles: files,
      id: this.updateObj ? this.updateObj?.id : null,
      noManufacturerCertificateFlag: body.noManufacturerCertificateFlag,
      noCustomImportPermitFlag: body.noCustomImportPermitFlag,
      noShipperImportPermitFlag: body.noShipperImportPermitFlag,
      noSourceTagImageFlag: body.noSourceTagImageFlag,
      noImagesFlag: body.noImagesFlag,
      noCharacterizationCertificateFlag: body.noCharacterizationCertificateFlag,
    };

    if (attachmentsStatus && this.addNewSourceForm.valid) {
      console.log(load);
      if (!this.updateObj) {
        this.nuclearMaterialServices
          .addNuclearMaterial(load)
          .subscribe((res) => {
            if (res['succeeded']) {
              this.app.messageService.add({
                severity: 'success',
                key: 'PlanValidation',
                summary: this.translateService.instant('submitedSuccessfully'),
                detail: this.translateService.instant('submitedSuccessfully'),
                life: 6000,
              });
              this.router.navigate(['nuclear-materials']);
            } else {
              this.app.messageService.add({
                severity: 'error',
                key: 'PlanValidation',
                summary: res['errors'],
                detail: res['errors'],
                life: 6000,
              });
            }
          });
      } else {
        this.nuclearMaterialServices
          .updateNuclearMaterial(load)
          .subscribe((res) => {
            if (res['succeeded']) {
              this.app.messageService.add({
                severity: 'success',
                key: 'PlanValidation',
                summary: this.translateService.instant('updatedSuccessfully'),
                detail: this.translateService.instant('updatedSuccessfully'),
                life: 6000,
              });
              this.router.navigate(['nuclear-materials']);
            } else {
              this.app.messageService.add({
                severity: 'error',
                key: 'PlanValidation',
                summary: res['errors'],
                detail: res['errors'],
                life: 6000,
              });
            }
          });
      }
    } else {
      this.app.messageService.add({
        severity: 'error',
        key: 'PlanValidation',
        summary: 'Error',
        detail: this.translateService.instant('validationMessage'),
        life: 6000,
      });
    }
  }

  SaveAsDraft() {
    let body = this.addNewSourceForm.value;
    console.log('body', body);
    this.app.messageService.add({
      severity: 'success',
      key: 'PlanValidation',
      summary: this.translateService.instant('savedAsDraft'),
      detail: this.translateService.instant('savedAsDraft'),
      life: 6000,
    });
  }

  updateSource() {}
}
