import { BaseFilter } from 'src/app/sharedFeatures/models/base-filter.model';

export class PendingApprovalRequest extends BaseFilter {
  employee!: string;
}
