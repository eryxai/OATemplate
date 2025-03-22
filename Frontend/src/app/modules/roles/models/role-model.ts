import { LookupModel } from 'src/app/sharedFeatures/models/lookup-model';

export class RoleViewModel {
  descriptionAr!: string;
  descriptionEn!: string;
  nameEn!: string;
  nameAr!: string;

  organizationId!: number;
  id!: number;

  permissions: any[] = [];
}
export class PermissionViewModel {
  id: any;
  name: any;
  checked: boolean;
}
