import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { EditorMode } from 'src/app/sharedFeatures/enum/editor-mode.enum';
import { LookupModel } from 'src/app/sharedFeatures/models/lookup-model';
import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';
import { RoleViewModel } from '../../models/role-model';
import { NameValueViewModel } from '../../models/role-permission-list.model';
import { RoleService } from '../../services/role.service';

@Component({
  selector: 'app-roles-add-edit',
  templateUrl: './roles-add-edit.component.html',
  styleUrls: ['./roles-add-edit.component.scss'],
})
export class RolesAddEditComponent implements OnInit, OnDestroy {
  PermissionsGroupList: any[] | undefined;
  SavedRolePermission!: NameValueViewModel[];
  language!: string;
  editForm!: FormGroup;
  subscriptionTranslate!: Subscription;
  OrganizationList: LookupModel[] | undefined;
  subscriptions: Subscription[] = [];
  imageSource: any;
  mode: EditorMode = EditorMode.add;
  id!: number;
  model!: RoleViewModel;
  domainUrl: string = this.roleService.domainUrl;
  isDisabled: boolean = false;
  checkAllPermission: boolean = false;
  allPermissionLoaded: boolean = false;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private translateService: TranslateService,
    private notificationService: NotificationService,
    private roleService: RoleService,
    private pageTitle: PageTitleService
  ) { }

  ngOnInit(): void {
    this.setPageTitle();
    this.buildForm();
    this.extractUrlParams();
    this.GetAllPermissionsGroups();
    this.setLanguageSubscriber();
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }
  extractUrlParams() {
    let editId = this.route.snapshot.params['editId'];
    if (editId) {
      this.mode = EditorMode.edit;
      this.id = editId;
    } else {
      this.mode = EditorMode.add;
    }
    if (this.id) {
      this.GetRolePermissions(this.id);
      this.subscriptions.push(
        this.roleService.getById(this.id).subscribe(
          (res: any) => {
            this.model = res;
            this.bindModelToForm();
          },
          (error: any) => {
            this.notificationService.showErrorTranslated(
              'error.shared.operationFailed',
              ''
            );
          }
        )
      );
    }
  }

  checkAllSubPermission(values: any, childid: any, groubid: any) {
    this.PermissionsGroupList?.forEach(group => {
      if (group.id == groubid) {
        group.children?.forEach((child: any) => {
          if (child.id == childid) {
            child.permissions?.forEach((perm: any) => {
              perm.checked = values.currentTarget.checked;
            });
            if (child !== undefined && child.permissions != undefined)
              child.checked = child.permissions?.every(
                (x: any) => x.checked === true
              );
          }
        });
      }
    });
    this.setCheckAll();
  }
  onChangePermission(
    values: any,
    permissionid: any,
    childid: any,
    groubid: any
  ) {

    this.PermissionsGroupList?.forEach(group => {
      if (group.id == groubid) {
        group.children?.forEach((child: any) => {
          if (child.id == childid) {
            child.permissions?.forEach((perm: any) => {
              if (perm.id == permissionid) {
                perm.checked = values.currentTarget.checked;
              }
            });
            if (child !== undefined && child.permissions != undefined) {
              child.checked = child.permissions?.every(
                (x: any) => x.checked === true
              );
            }
          }
        });
        
        group.permissions?.forEach((perm: any) => {
          if (perm.id == permissionid) {
            perm.checked = values.currentTarget.checked;
          }
        });
      }
    });

    this.setCheckAll();
  }
  submit(): void {
    this.isDisabled = true;
    if (this.editForm.valid) {
      let role: RoleViewModel = this.collectModel();
      if (role.permissions.length > 0) {
        if (this.id) {
          this.subscriptions.push(
            this.roleService.update(role).subscribe(
              (res: any) => {
                this.notificationService.showSuccessTranslated(
                  'shared.dataUpdatedSuccessfuly',
                  ''
                );
                this.isDisabled = false;
                this.goToList();
              },
              (error: any) => {
                this.isDisabled = false;

                this.notificationService.showErrorTranslated(
                  'error.shared.operationFailed',
                  ''
                );
              }
            )
          );
        } else {
          this.subscriptions.push(
            this.roleService.add(role).subscribe(
              (res: any) => {
                this.notificationService.showSuccessTranslated(
                  'shared.dataSavedSuccessfuly',
                  ''
                );
                this.isDisabled = false;
                this.goToList();
              },
              (error: any) => {
                this.isDisabled = false;

                if (error.error == 400) {
                  let key = 'error.400';
                  this.translateService.get([key]).subscribe(res => {
                    this.notificationService.showErrorTranslated(
                      `${res[key]}`,
                      ''
                    );
                  });
                } else {
                  if ((error.error = 20)) {
                    this.notificationService.showErrorTranslated(
                      `error.${error.error}`,
                      ''
                    );
                  } else {
                    this.notificationService.showErrorTranslated(
                      'error.shared.operationFailed',
                      ''
                    );
                  }
                }
              }
            )
          );
        }
      } else {
        this.isDisabled = false;
        this.notificationService.showErrorTranslated(
          'error.shared.selectAtleastOnePermission',
          ''
        );
      }
    } else {
      this.isDisabled = false;
      const loginFormFormKeys = Object.keys(this.editForm.controls);
      loginFormFormKeys.forEach(control => {
        this.editForm.controls[control].markAsTouched();
      });
    }
  }
  setCheckAll() {
    var checkedCount = 0;
    var childrenCount = 0;
    this.PermissionsGroupList?.forEach(group => {
      childrenCount = childrenCount + group.children.length;
      group.children.forEach((child: any) => {
        if (child !== undefined && child.permissions != undefined)
          if (child.permissions?.every((x: any) => x.checked === true)) {
            checkedCount++;
          }
      });
    });
    if (childrenCount == checkedCount) {
      this.checkAllPermission = true;
    } else {
      this.checkAllPermission = false;
    }
  }
  GetAllPermissionsGroups() {
    this.subscriptions.push(
      this.roleService.GetAllPermissionsGroups().subscribe(
        res => {
          this.PermissionsGroupList = [];
          let list = res;
          list.forEach(group => {
            if (group.parentId) {

            } else {
              this.PermissionsGroupList?.push(group);
            }
          });
          this.PermissionsGroupList?.forEach(parent => {
            parent.children = [];
            list.forEach(group => {
              if (group.parentId) {
                if (group.parentId == parent.id) {
                  parent.children.push(group);
                }
              }
              this.getPermission(group);
            });
          });
          if (this.mode == EditorMode.add) {
            this.PermissionsGroupList?.forEach(group => {
              group.children.forEach((child: any) => {
                child = this.getPermission(child);
              });
            });
            this.allPermissionLoaded = true;
          }
        },
        error => {
          this.notificationService.showError(error.message, 'error');
        }
      )
    );
  }

  GetRolePermissions(id: any) {
    this.subscriptions.push(
      this.roleService.GetRolePermission(id).subscribe(
        res => {

          this.SavedRolePermission = res.list;
          this.PermissionsGroupList?.forEach(group => {


            group.children.forEach((child: any) => {


              child = this.getPermission(child);
            });
          });
          this.allPermissionLoaded = true;
        },
        error => {
          this.notificationService.showError(error.message, 'error');
        }
      )
    );
  }

  getPermission(group: any): any {
    if (!group?.permissions) {
      this.subscriptions.push(
        this.roleService.GetPermissionByGroupId(group.id).subscribe(
          (res: LookupModel[]) => {
            if (this.SavedRolePermission?.length > 0) {
              res.forEach((permission: any) => {
                if (
                  this.SavedRolePermission.some(x => x.value == permission.id)
                ) {

                  permission.checked = true;
                }
              });
            }
            group.permissions = res;


            if (group !== undefined && group.permissions != undefined)
              group.checked = group.permissions?.every(
                (x: any) => x.checked === true
              );

            this.setCheckAll();
          },
          error => {
            this.notificationService.showError(error.message, 'error');
            return [];
          }
        )
      );
    } else {
      if (this.SavedRolePermission?.length > 0) {
        group.permissions.forEach((permission: any) => {
          if (this.SavedRolePermission.some(x => x.value == permission.id)) {
            permission.checked = true;
          }
        });
      }
    }
  }
  checkAllPermissionChange(values: any) {
    
    this.PermissionsGroupList?.forEach(group => {
      if(group.children?.length>0){
      group.children?.forEach((child: any) => {
        this.checkAllPermission ==
          group.children?.every((child: any) =>
            child.permissions?.every((perm: any) => perm.checked)
          );
        child.permissions?.forEach((permission: any) => {
          permission.checked = values.currentTarget.checked;
        });
        if (child !== undefined && child.permissions != undefined)
          child.checked = child.permissions?.every(
            (x: any) => x.checked === true
          );
      });
    }else{
      group.permissions?.forEach((permission: any) => {
        permission.checked = values.currentTarget.checked;
      });
      if (group !== undefined && group.permissions != undefined)
        group.checked = group.permissions?.every(
          (x: any) => x.checked === true
        );
    }

    });
  }

  collectModel(): RoleViewModel {
    let role = new RoleViewModel();
    role.id = this.id;
    role.descriptionAr = this.editForm?.controls['descriptionAr'].value;
    role.descriptionEn = this.editForm?.controls['descriptionEn'].value;
    role.nameAr = this.editForm?.controls['nameAr'].value;
    role.nameEn = this.editForm?.controls['nameEn'].value;

    let permissions: any[] = [];

    this.PermissionsGroupList?.forEach(group => {
      group.children?.forEach((child: any) => {
        child.permissions?.forEach((permission: any) => {
          if (permission?.checked) {
            permissions.push(permission);
          }
        });
      });
      group.permissions?.forEach((permission: any) => {
        if (permission?.checked) {
          permissions.push(permission);
        }
      });
    });
    role.permissions = permissions;
    return role;
  }
  bindModelToForm() {
    this.editForm?.controls['nameAr']?.setValue(this.model.nameAr);
    this.editForm?.controls['nameEn']?.setValue(this.model.nameEn);
    this.editForm?.controls['descriptionAr']?.setValue(
      this.model.descriptionAr
    );
    this.editForm?.controls['descriptionEn']?.setValue(
      this.model.descriptionEn
    );
  }
  buildForm(): void {
    this.editForm = this.fb.group({
      nameEn: [null, [Validators.required]],
      nameAr: [null, [Validators.required, Validators.maxLength(200)]],
      descriptionAr: ['', [Validators.maxLength(200)]],
      descriptionEn: ['', [Validators.maxLength(200)]],
    });
  }
  setLanguageSubscriber(): void {
    this.language = this.translateService.currentLang;
    this.subscriptionTranslate = this.translateService.onLangChange.subscribe(
      val => {
        this.language = val.lang;
        this.setPageTitle();
        this.GetAllPermissionsGroups();
      },
      error => { },
      () => { }
    );
    this.subscriptions.push(this.subscriptionTranslate);
  }
  setPageTitle(): void {
    this.pageTitle.setTitleTranslated(`Role.addNewRole`);
  }
  getControl(controlName: string): any {
    return this.editForm?.controls[controlName];
  }
  goToList() {
    this.router.navigate(['/roles/list']);
  }
}
