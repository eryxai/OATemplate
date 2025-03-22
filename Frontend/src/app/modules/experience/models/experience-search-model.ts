export class ExperienceSearchModel {
  minCreationDate?: Date;
  maxCreationDate?: Date;
  minFirstModificationDate?: Date;
  maxFirstModificationDate?: Date;
  minLastModificationDate?: Date;
  maxLastModificationDate?: Date;
  createdByUserId?: number;
  firstModifiedByUserId?: number;
  lastModifiedByUserId?: number;
  isDeleted: boolean;
  minDeletionDate?: Date;
  maxDeletionDate?: Date;
  deletedByUserId?: number;
  mustDeletedPhysical?: boolean;
  name!: string;
  parentId!: number | null;
  parentName!: string;
}
