import { Route, Routes } from 'react-router-dom';
import { Home } from './pages/Home';
import 'bootstrap/dist/css/bootstrap.min.css';
import Topics from './pages/Topics';
import { NotFound } from './pages/NotFound';
import { Editor } from './pages/Editor';


function App() {
  return (
   
      <Routes>
        <Route path='*' element={<NotFound/>}/>
        <Route path='/' element={<Home />} />
        <Route path='/Topics' element={<Topics />} />
        <Route path='Editor' element={<Editor/>}/>
      </Routes>
  );
}

export default App;