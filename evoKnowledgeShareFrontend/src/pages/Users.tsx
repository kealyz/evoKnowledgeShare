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
  

    //ez minden hol
    useEffect(() => {
      const data = async() => {
        const res = await fetch('https://localhost:5145/api/User');
        const json = await res.json();
        setUsers(json); 
      }
      data();
    }, [])
  
    function GetTopics() {
      return (
        <>
          {users?.map((user) => {
            return (<p key={user.id}>{user.id}: {user.firstName}</p>)
          })}
        </>
      )
    }
  
    return (
      <>
        <motion.div variants={containerVariant}
          initial="hidden"
          animate="visible">
          <RenderTable topics={users} />
        </motion.div>
      </>
    )
}
