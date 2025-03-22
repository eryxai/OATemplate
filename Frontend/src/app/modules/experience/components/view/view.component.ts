import { Component, OnInit, OnDestroy } from '@angular/core';
import { ExperienceService } from '../../services/experience.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';
import { ExperienceViewModel } from '../../models/experience-view-model';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.scss'],
})
export class ExperienceViewComponent implements OnInit,OnDestroy {
  formGroup!: FormGroup;
  model: ExperienceViewModel = new ExperienceViewModel();
  parentLookup: any[];
  subscriptions: Subscription[] = [];

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private experienceService: ExperienceService,
    private pageTitle: PageTitleService
  ) {}

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
    }));
  }
  buildForm(): void {
    this.formGroup = this.fb.group({
      nameEn: [null, []],

      nameAr: [null, []],
      parentId: [null, []],
    });
  }
  bindModelToForm() {
    this.formGroup?.controls['nameEn']?.patchValue(this.model.nameEn);
    this.formGroup?.controls['nameAr']?.patchValue(this.model.nameAr);
    this.formGroup?.controls['parentId']?.patchValue(this.model.parentId);
  }

  setPageTitle(): void {
    this.pageTitle.setTitleTranslated(`experience.view`);
  }

  pageIndex: number = 0;
  pageSize: number = 0;

  extractUrlParams() {
    this.subscriptions.push(
    this.route.queryParams.subscribe(params => {
      this.pageIndex = params['pageIndex'];
      // debugger;
      this.pageSize = params['pageSize'];
    }));
    let editId = this.route.snapshot.params['id'];
    if (editId) {
      this.subscriptions.push(
      this.experienceService.getByID(editId).subscribe(result => {
       this.model = result;
        this.bindModelToForm();
      }));
    }
  }
  getControl(controlName: string): any {
    return this.formGroup?.controls[controlName];
  }

  backToList() {
    const pageSize = this.pageSize;
    const pageIndex = this.pageIndex;
    // Pass the pagination parameters back to the list component when navigating back
    this.router.navigate(['/experience/list'], {
      queryParams: { pageIndex, pageSize },
    });
  }

  ngOnDestroy(){
    this.subscriptions.forEach(s => s.unsubscribe());  }
}
