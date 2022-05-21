use std::ffi::OsStr;
use std::fmt::Debug;
use std::path::{Path, PathBuf};

pub fn path_exists(path: &str) -> bool {
    Path::new(path).exists()
}

pub fn filter_files(dir: &str, ext: &str) -> Vec<PathBuf> {
    let mut faxvec: Vec<PathBuf> = Vec::new();
    for element in std::path::Path::new(dir).read_dir().unwrap() {
        let path = element.unwrap().path();
        if let Some(extension) = path.extension() {
            if extension == ext {
                faxvec.push(path);
            }
        }
    }
    return faxvec;
}