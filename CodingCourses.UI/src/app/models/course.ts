import { Topic } from "./topic";

export class Course {
    id?: number;
    name = "";
    description = "";
    topics: Topic[] = [];

    editMode: boolean = false;
}
