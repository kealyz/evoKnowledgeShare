import { motion } from 'framer-motion'
import React, { useState } from 'react'
import { useSelector } from 'react-redux'
import { useDispatch } from 'react-redux'
import { RootState } from '../store'
import { modalActions } from '../store/modal'
import { Modal } from '../ui/Modal'
import NavMenu from '../ui/NavMenu'

export const Layout = (props: any) => {

    return (
        <>
            <NavMenu />
            <main style={{ margin: "2rem" }}>
                {props.children}
            </main>
        </>
    )
}
