import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'amDateFormat' })
export class amDateFormat implements PipeTransform {
  transform(value: Date | moment.Moment, dateFormat: string): any {
    return moment(value).format(dateFormat);
  }
}
