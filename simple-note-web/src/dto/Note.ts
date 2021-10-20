class Note {
  id: number;
  text: string;

  constructor(fields?: Partial<Note>) {
    this.id = fields?.id ?? 0;
    this.text = fields?.text ?? "";
  }
}

export default Note;