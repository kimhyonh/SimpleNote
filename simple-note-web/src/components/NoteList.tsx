import React from "react";
import Note from "../dto/Note";

interface NoteListProps {
  notes: Note[];
  onClick: (note: Note) => void;
  onDelete: (note: Note) => void;
};

class NoteList extends React.Component<NoteListProps> {

  render(): JSX.Element {
    const notes = this.props.notes ?? [];
    return (
      <div>
        {notes.map(x => 
          <div key={x.id}>
            <p onClick={e => this.props.onClick?.(x)}>{x.text}</p>
            <button onClick={e => this.props.onDelete?.(x)}>Delete</button>
          </div>
        )}
      </div>
    );
  }
}

export default NoteList;