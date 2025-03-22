import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'msn-input-image',
  templateUrl: './input-image.component.html',
  styleUrls: ['./input-image.component.scss'],
})
export class InputImageComponent {
  @Output() inputFileChanged: EventEmitter<any> = new EventEmitter();
  @Output() file: EventEmitter<any> = new EventEmitter();
  @Input() url: any = '';
  @Input() name: string = '';
  @Input() viewImage: boolean = true;
  @Input() accept: string = 'image/*';
  constructor() {}

  onUploadImage(event: any): void {
    if (event.target.files && event.target.files[0]) {
      this.file.emit(event.target.files[0]);
      const reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = () => {
        const fileName = event.target.files[0].name;
        const path = reader.result as string;
        const extension = event.target.files[0].type.split('/')[1];
        this.url = path;
        const file = { fileName, path, extension };
        this.inputFileChanged.emit(file);
      };
    }
  }
  removeImage(): void {
    this.url = '';
    this.inputFileChanged.emit(this.url);
  }
}
