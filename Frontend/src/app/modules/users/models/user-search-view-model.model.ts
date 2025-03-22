import { BaseFilter } from 'src/app/sharedFeatures/models/base-filter.model';

export class UserSearchViewModel extends BaseFilter {
  username!: string;
  name!: string;
  email!: string;
  phoneNumber!: string;
  organizationId!: string;
  departmentId!: string;
  isActive!: string;
  nameEmail!: string;
}
