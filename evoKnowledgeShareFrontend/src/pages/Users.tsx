import { motion } from 'framer-motion'
import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import RenderTable from '../components/ObjectTable'
import IUser from '../interfaces/IUser'
import { modalActions } from '../store/modal';
import { Modal } from '../ui/Modal';
import { RootState } from '../store'
import { Button } from 'react-bootstrap'

/*
- nincs user
- filter komponens
- history bekötés -> history generálás
*/

/*
- user
- log
- auth
*/

/*
23.prezentáció 4 óra
- bevezető - Kelemen Bence
- backend - Peti
- frontend - Marci
- demo - Szabó András 
- jövő - Bence
- kérdések
*/

/*
Topic-ot csak akkor lehessen törölni, ha nem tartozik hozzá egyetlen note sem, 
csekkolni service-ben

User-eket adatbázis szintjén tárolni (mock adatok)

History-createnél majd csak egy létező userhez kellene bekötni. Ez külön branch 
lesz majd a végén (demo branch)
*/

export const Users = () => {
  const dispatch = useDispatch();
  let modalIsShown = useSelector((state: RootState) => state.modal.show);
  const modalContent = useSelector((state: RootState) => state.modal.content);
  const [users, setUsers] = useState<IUser[]>([])
  const [userId, setUserId] = useState<string>();

  const containerVariant = {
    hidden: {
      opacity: 0
    },
    visible: {
      opacity: 1,
      transition: {
        duration: 0.2
      }
    }
  }

  const showModalHandler = (content: string) => {
    dispatch(modalActions.setContent(content))
    dispatch(modalActions.toggleShow())
  }

  const hideModalHandler = () => {
    dispatch(modalActions.toggleShow());
    dispatch(modalActions.removeModalContent);
  }

  const fetchUser = async () => {
    const getAllUser = await fetch('https://localhost:5145/api/User');
    setUsers(await getAllUser.json());
  };

  //This is only temporary 
  useEffect(() => {
    fetchUser();
  }, [users])

  const deleteUser = async () => {
    console.log(userId)
    const response = await fetch(`https://localhost:7145/api/User/${userId}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      },
    });
    setUserId('');
    hideModalHandler();
  }

  const onDeleteUser = async (userId: string) => {
    showModalHandler("Are you want to delete " + userId + " ?");
    setUserId(userId);
  }

  return (
    <>
      {modalIsShown && (
        <Modal onClose={hideModalHandler}>
          <div className="">
            <p>{modalContent}</p>
            <Button className='m-2' variant="success" onClick={deleteUser}>Yes</Button>
            <Button variant="danger" onClick={hideModalHandler}>No</Button>
          </div>
        </Modal>
      )}

      <motion.div variants={containerVariant}
        initial="hidden"
        animate="visible">
        <h1 className='mb-4'>User operations</h1>
        {(users && Object.keys(users).length !== 0) ? <RenderTable data={users} onDelete={onDeleteUser} /> : <h2>No user</h2>}
      </motion.div>
    </>
  )
}