import { BaseFilter } from 'src/app/sharedFeatures/models/base-filter.model';

export class OrganizationlookupSearchModel extends BaseFilter {
  name!: string;
  paginated!: boolean;
}

export class DepartmentlookupSearchModel extends BaseFilter {
  name!: string;
 
  paginated!: boolean;
}

export class RolelookupSearchModel extends BaseFilter {
  name!: string;
  organizationId!: number;
  paginated!: boolean;
}
export class AdditionDoclookupSearchModel extends BaseFilter {

  supplierId!: number;
 
  paginated!: boolean;
}
export class AdditionDocItemlookupSearchModel extends BaseFilter {

  itemsId: number;
  unitId:number;
  categoryId:number;
  paginated!: boolean;
}