import React from "react";
import { Button } from "react-bootstrap";
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
      <ul className="list-group my-2">
        {notes.map(x => 
          <li className="list-group-item" key={x.id}>    
            <div className="row">       
              <div className="col-8">   
                <span>{x.text}</span>
              </div>
              <div className="col-4 text-end">
                <Button className="mx-1" size="sm" variant="info" onClick={e => this.props.onClick?.(x)}>Edit</Button>
                <Button className="mx-1" size="sm" variant="danger" onClick={e => this.props.onDelete?.(x)}>Delete</Button>
              </div>
            </div>
          </li>
        )}
      </ul>
    );
  }
}

export default NoteList;