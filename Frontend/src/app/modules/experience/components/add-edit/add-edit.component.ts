import { Component, OnInit, OnDestroy } from '@angular/core';
import { ExperienceService } from '../../services/experience.service';
import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';
import { ExperienceViewModel } from '../../models/experience-view-model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.scss'],
})
export class ExperienceAddEditComponent implements OnInit, OnDestroy {
  model: ExperienceViewModel = new ExperienceViewModel();

  typeId: number;
  parentLookup: any[];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private experienceService: ExperienceService,
    private notificationService: NotificationService,
    private pageTitle: PageTitleService
  ) {}

  formGroup!: FormGroup;
  subscriptions: Subscription[] = [];

  ngOnInit(): void {
    this.buildForm();
    this.extractUrlParams();
    this.setPageTitle();
    this.getInstructorsLookup();
  }
  getInstructorsLookup() {
    this.subscriptions.push(
      this.experienceService.getLookUp().subscribe(result => {
        this.parentLookup = result;
      })
    );
  }

  setPageTitle(): void {
    this.typeId = this.route.snapshot.params['editId'];
    if (this.typeId) {
      this.pageTitle.setTitleTranslated(`experience.edit`);
    } else {
      // debugger;
      this.pageTitle.setTitleTranslated(`experience.addNew`);
    }
  }
  buildForm(): void {
    this.formGroup = this.fb.group({
      nameEn: [null, [Validators.required, Validators.maxLength(250)]],

      nameAr: [null, [Validators.required, Validators.maxLength(250)]],
      parentId: [null, []],
    });
  }
  bindModelToForm() {
    this.formGroup?.controls['nameEn']?.patchValue(this.model.nameEn);
    this.formGroup?.controls['nameAr']?.patchValue(this.model.nameAr);
    this.formGroup?.controls['parentId']?.patchValue(this.model.parentId);
  }
  extractUrlParams() {
    let editId = this.route.snapshot.params['editId'];

    if (editId) {
      this.subscriptions.push(
        this.experienceService.getByID(editId).subscribe(result => {
          this.model = result;

          this.bindModelToForm();
        })
      );
    }
  }
  getControl(controlName: string): any {
    return this.formGroup?.controls[controlName];
  }
  backToList() {
    this.router.navigate(['/experience/list']);
  }
  Save() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.invalid) return;
    this.collectModel();
    if (this.model.id == null || this.model.id?.toString() == '0') {
      this.subscriptions.push(
        this.experienceService.AddExperience(this.model).subscribe(result => {
          this.notificationService.showSuccessTranslated(
            '',
            'shared.dataSavedSuccessfuly'
          );
          this.router.navigate(['/experience/list']);
        })
      );
    } else {
      this.subscriptions.push(
        this.experienceService.Update(this.model).subscribe(result => {
          this.notificationService.showSuccessTranslated(
            '',
            'shared.dataUpdatedSuccessfuly'
          );
          this.router.navigate(['/experience/list']);
        })
      );
    }
  }
  collectModel() {
    this.model.nameAr = this.formGroup.value.nameAr;
    this.model.nameEn = this.formGroup.value.nameEn;
    this.model.parentId = this.formGroup.value.parentId;
  }

  ngOnDestroy() {
    this.subscriptions.forEach(s => s.unsubscribe());
  }
}
