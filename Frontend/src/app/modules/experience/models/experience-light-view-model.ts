export class ExperienceLightViewModel {
  id!: number;
  creationDate!: string;
  firstModificationDate!: string | null;
  lastModificationDate!: string | null;
  createdByUserId!: number | null;
  firstModifiedByUserId!: number | null;
  lastModifiedByUserId!: number | null;
  isDeleted!: boolean;
  deletionDate!: string | null;
  deletedByUserId!: number | null;
  mustDeletedPhysical!: boolean | null;
  name!: string;
  parentId!: number | null;
  parentName!: string;
}
