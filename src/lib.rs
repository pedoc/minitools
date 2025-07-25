use std::ffi::OsStr;
use std::fmt::{Debug, format};
use std::path::{Path, PathBuf};
use std::fs::File;
use std::io::{self, BufRead};

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

pub fn read_lines<P>(filename: P) -> io::Result<io::Lines<io::BufReader<File>>>
    where P: AsRef<Path>, {
    let file = File::open(filename)?;
    Ok(io::BufReader::new(file).lines())
}