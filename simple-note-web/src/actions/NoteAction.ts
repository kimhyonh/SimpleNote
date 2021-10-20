import axios from "axios";
import Note from "../dto/Note";

class NoteAction {

  private readonly baseUrl = "https://localhost:44338/api";

  public async getAllNotes(): Promise<Note[]> {
    return (await this.get<Note[]>("Note")) ?? [];
  }

  public async create(text: string): Promise<void> {
    try {
      await axios.post(`${this.baseUrl}/Note`, text, { headers: {'Content-Type': 'application/json'}});
    } catch (error) {
      console.error(error);
    }
  }
  
  public async update(id: number, text: string): Promise<void> {
    try {
      await axios.put(`${this.baseUrl}/Note/${id}`, text, { headers: {'Content-Type': 'application/json'}});
    } catch (error) {
      console.error(error);
    }
  }

  public async delete(id: number): Promise<void> {
    try {
      await axios.delete(`${this.baseUrl}/Note/${id}`);
    } catch (error) {
      console.error(error);
    }
  }

  private async get<T>(endpoint: string): Promise<T | null> {
    try {
      const response = await axios.get<T>(`${this.baseUrl}/${endpoint}`);
      return response.data;
    } catch(error) {
      console.error(error);
    }
    return null;
  }
}

export default NoteAction;