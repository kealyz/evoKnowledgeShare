import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Home from './Home';
import NavMenu from './NavMenu';

import 'bootstrap/dist/css/bootstrap.min.css';
import Topics from './Topics';

function App() {
  return (
    <BrowserRouter>
      <NavMenu />
      <Routes>
        <Route path='/' element={<Home />} />
        <Route path='/Topics' element={<Topics />} />
      </Routes>
    </BrowserRouter>
  );
}
export default App;