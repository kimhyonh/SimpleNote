import { useState } from "react";

type NoteViewProps = {
  text: string;
  onSave: (value: string) => void;
}

const NoteView = (props: NoteViewProps) => {
  const [text, setText] = useState(props.text);

  return (
    <div>
      <textarea
        value={text}
        onChange={e => setText(e.target.value)}
      />
      <button onClick={e => props.onSave?.(text)}>Save</button>
    </div>
  );
};

export default NoteView;