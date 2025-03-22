import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'delete-modal',
  templateUrl: './delete-modal.component.html',
  styleUrls: ['./delete-modal.component.sass'],
})
export class DeleteModalComponent implements OnInit {
  deleteMessage: string = '';
  deleteAction!: () => void;

  constructor(
    public dynamicDialogRef: DynamicDialogRef,
    public config: DynamicDialogConfig
  ) {}

  ngOnInit(): void {
    this.deleteMessage = this.config.data.bodyMessage;
    document.addEventListener('keydown', this.preventTab);

    this.deleteAction = this.config.data.deleteAction;
  }
  preventTab(e: any) {
    e = e || window.event;
    if (e.keyCode === 9 || e.keyCode === 13) {
      // If tab key is pressed
      e.preventDefault(); // Stop event from its action
    }
  }
  close(): void {
    document.removeEventListener('keydown', this.preventTab);
    this.dynamicDialogRef.close();
  }
}
