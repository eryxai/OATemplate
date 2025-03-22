import { Injectable, Type } from '@angular/core';
import {
  DialogService,
  DynamicDialogConfig,
  DynamicDialogRef,
} from 'primeng/dynamicdialog';

@Injectable({
  providedIn: 'root',
})
export class DynamicDialogService {
  currentResponsiveClass: string = '';
  constructor(private dialogService: DialogService) {}
  showDynamicDialog(
    componentRef: Type<any>,
    config: DynamicDialogConfig
  ): DynamicDialogRef {
    return this.dialogService.open(componentRef, config);
  }
}
