import { Author } from "./model.author";

export class Book implements IdentityModel {
  public Id: number = 0;
  public Title: string = '';
  public PageCount: number = 1;
  public Publisher: string;
  public IssueDate: Date;
  public ISBN: string;
  public Authors: Author[] = [];
  public Image: string;
}
