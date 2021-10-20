import { useState } from "react";
import { Button } from 'react-bootstrap';

type NoteViewProps = {
  text: string;
  onSave: (value: string) => void;
  onCancel: () => void;
}

const NoteView = (props: NoteViewProps) => {
  const [text, setText] = useState(props.text);

  return (
    <div>
      <textarea
        className="form form-control my-2"
        value={text}
        onChange={e => setText(e.target.value)}
      />
      <Button className="me-2" variant="primary" onClick={e => props.onSave?.(text)}>Save</Button>
      <Button variant="secondary" onClick={e => props.onCancel?.()}>Cancel</Button>
    </div>
  );
};

export default NoteView;