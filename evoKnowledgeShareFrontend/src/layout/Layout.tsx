import { motion } from 'framer-motion'
import React from 'react'
import NavMenu from '../ui/NavMenu'



export const Layout = (props: any) => {
    return (
        <>
            <NavMenu />
            <main >
                {props.children}
            </main>
        </>
    )
}
