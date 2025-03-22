import { AttachmentTypeEnum } from '../enum/AttachmentTypeEnum';

export class GeneralAttachmentViewModel {
  id: number;
  fileName: string;
  path: string;
  extention: string;
  attachmentType: AttachmentTypeEnum;
  objectId: number;
  /// Image, video, Doc, Voice, PDF
  /// </summary>
  contentType: number;
}
