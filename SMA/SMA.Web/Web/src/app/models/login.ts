import { Usuario } from '../models/usuario';
import { Menu } from '../models/menu';

export class Login {
  public usuario: Usuario;
  public token: string;
  public menu: Menu[];
}
