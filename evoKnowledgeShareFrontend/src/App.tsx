import { Route, Routes } from 'react-router-dom';
import { Home } from './pages/Home';
import 'bootstrap/dist/css/bootstrap.min.css';
import Topics from './pages/Topics';
import { NotFound } from './pages/NotFound';
import { Editor } from './pages/Editor';
import { Histories } from './pages/Histories';
import { Users } from './pages/Users';
import { Notes } from './pages/Notes';
import { UpdateNote } from './pages/UpdateNote';

function App() {
  return (
   
      <Routes>
        <Route path='*' element={<NotFound/>}/>
        <Route path='/' element={<Home />} />
        <Route path='/Topics' element={<Topics />} />
        <Route path='/Editor' element={<Editor/>}/>
        <Route path='/Histories' element={<Histories/>}/>
        <Route path='/Users' element={<Users/>}/>
        <Route path='/Notes' element={<Notes/>}/>
        <Route path='/UpdateNote/:id' element={<UpdateNote/>}/>
      </Routes>
  );
}

export default App;