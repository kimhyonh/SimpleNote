import React from "react";
import { Button } from "react-bootstrap";
import NoteAction from "../actions/NoteAction";
import Note from "../dto/Note";
import NoteList from "./NoteList";
import NoteView from "./NoteView";

type NoteComponentState = {
  notes: Note[],
  currentViewNote: Note | null
}

class NoteComponent extends React.Component<{}, NoteComponentState> {

  private readonly action = new NoteAction();

  state: NoteComponentState = {
    notes: [],
    currentViewNote: null
  };

  componentDidMount(): void {
    this.loadNotes();
  }

  render(): JSX.Element {
    return (
      <div style={{ padding: "30px 50px" }}>
        <h3 className="text-center">Simple Note</h3>
        <hr/>

        {this.state.currentViewNote &&
          <NoteView
            text={this.state.currentViewNote.text}
            onSave={value => this.handleOnNoteSave(value)}
            onCancel={() => this.mutateState(s => s.currentViewNote = null)}
          />
        }

        {!this.state.currentViewNote &&
          <div>
            <NoteList
              notes={this.state.notes}
              onClick={note => this.handleOnNoteClick(note)}
              onDelete={note => this.handleOnDelete(note)}
            />
            
            <Button onClick={e => this.handleOnCreate()}>Create</Button>
          </div>
        }
      </div>
    );
  }

  private mutateState(mutation: (state: NoteComponentState) => void, then?: () => void) {
    this.setState(s => {
      mutation(s);
      return s;
    }, then);
  }

  private handleOnCreate(): void {
    this.mutateState(s => s.currentViewNote = new Note());
  }

  private handleOnNoteSave(value: string): void {
    if (!this.state.currentViewNote)
      return;

    if (this.state.currentViewNote.id > 0) {
      this.action
        .update(this.state.currentViewNote.id, value)
        .then(response => this.loadNotes());
    } else {
      this.action
        .create(value)
        .then(response => this.loadNotes());
    }
  }

  private handleOnNoteClick(note: Note): void {
    this.mutateState(s => s.currentViewNote = note);
  }

  private handleOnDelete(note: Note): void {
    this.action
      .delete(note.id)
      .then(response => this.loadNotes());
  }
  
  private loadNotes(): void {
    this.action
      .getAllNotes()
      .then(notes =>
        this.mutateState(s => {
          s.currentViewNote = null;
          s.notes = notes;
        })
      );
  }
}

export default NoteComponent;