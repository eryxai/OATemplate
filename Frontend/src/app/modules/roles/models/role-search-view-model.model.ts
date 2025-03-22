import { BaseFilter } from 'src/app/sharedFeatures/models/base-filter.model';

export class RoleSearchViewModel extends BaseFilter {
  name!: string;
  organizationId!: string;
}
