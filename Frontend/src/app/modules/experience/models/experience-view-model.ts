export class ExperienceViewModel {
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
  nameEn!: string;
  nameAr!: string;

  parentId!: number | null;
  parentName!: string;

}
