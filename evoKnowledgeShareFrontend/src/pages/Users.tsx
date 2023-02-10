import { motion } from 'framer-motion'
import React, { useEffect, useState } from 'react'
import RenderTable from '../components/ObjectTable'
import IUser from '../interfaces/IUser'

export const Users = () => {
  const [users, setUsers] = useState<IUser[]>([])

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

  useEffect(() => {
    const data = async () => {
      const res = await fetch('https://localhost:5145/api/User');
      const json = await res.json();
      setUsers(json);
    }
    data();
  }, [])

  const deleteUser = async (userId:string) => {
    const response = await fetch(`https://localhost:7145/api/User/${userId}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      },
    });
    const data = await response.json();
    console.log(data)
    if (!response.ok) {
      throw new Error(data.message || 'Error');
    }
  }

  

return (
  <>
    <motion.div variants={containerVariant}
      initial="hidden"
      animate="visible">
      <h1 className='mb-4'>User operations</h1>
      <RenderTable topics={users} viewable={true} editable={true} deletable={true} onDelete={deleteUser}/>
    </motion.div>
  </>
)
}
